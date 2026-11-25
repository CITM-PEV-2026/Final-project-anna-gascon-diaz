using UnityEngine;
using UnityEngine.Events; //Para los eventos que se configuran desde el inspector.
using UnityEngine.InputSystem;

public class Interactuable : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference interactAction;   // Para arrastrar el Player/Interact (tecla E)

    [Header("Acción")]
    public string nombreAccion = "Activar bio-filtros";
    public float valor = 0.2f;          // positivo = utopía, negativo = distopía
    public float tiempoEspera = 5f;     // segundos antes de poder repetirla

    [Header("Efecto local (se conecta desde el inspector)")]
    public UnityEvent alInteractuar; // Se conecta lo que debe pasar en el objeto al usarlo.

    bool jugadoraCerca = false;
    float ultimoUso = -999f; //Guarda el momento en que se usó por última vez. Empieza en −999 para que la primera vez siempre se pueda usar.

    void OnEnable()  { if (interactAction != null) interactAction.action.Enable(); }
    void OnDisable() { if (interactAction != null) interactAction.action.Disable(); }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return; //comprueba si es la jugadora. El ! significa "no": si no es la jugadora, sale.
        jugadoraCerca = true;
        Debug.Log("E — " + nombreAccion);   // marca que está cerca y escribe en la consola qué acción puede hacer. más adelante P3 lo mostrará en pantalla
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) jugadoraCerca = false;
    }

    void Update()
    {
        if (!jugadoraCerca || interactAction == null) return; //Comprueba que la jugadora esté cerca y que haya una acción asignada.
        if (!interactAction.action.WasPressedThisFrame()) return; //Comprueba si se acaba de pulsar la E en este frame.

        if (Time.time - ultimoUso < tiempoEspera) //Si desde el último uso han pasado menos segundos de los que hay que esperar, avisa y no hace nada (cooldown).
        {
            Debug.Log(nombreAccion + ": todavía no se puede repetir");
            return;
        }

        ultimoUso = Time.time;
        alInteractuar.Invoke();                              // ejecuta todo lo conectado en el inspector
        WorldStateManager.Instance.AddModifier(valor);       // Llama al World Manager para sumar el valor
    }
}