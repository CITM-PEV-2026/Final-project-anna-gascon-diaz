using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interactuable : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference interactAction;   // Player/Interact

    [Header("Acción")]
    public string nombreAccion = "Activar bio-filtros";
    public float valor = 0.2f;          // positivo = utopía, negativo = distopía
    public float tiempoEspera = 5f;     // segundos antes de poder repetirla

    [Header("Efecto local (se conecta desde el inspector)")]
    public UnityEvent alInteractuar;

    bool jugadoraCerca = false;
    float ultimoUso = -999f;

    void OnEnable()  { if (interactAction != null) interactAction.action.Enable(); }
    void OnDisable() { if (interactAction != null) interactAction.action.Disable(); }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        jugadoraCerca = true;
        Debug.Log("E — " + nombreAccion);   // más adelante P3 lo mostrará en pantalla
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) jugadoraCerca = false;
    }

    void Update()
    {
        if (!jugadoraCerca || interactAction == null) return;
        if (!interactAction.action.WasPressedThisFrame()) return;

        if (Time.time - ultimoUso < tiempoEspera)
        {
            Debug.Log(nombreAccion + ": todavía no se puede repetir");
            return;
        }

        ultimoUso = Time.time;
        alInteractuar.Invoke();                              // efecto local
        WorldStateManager.Instance.AddModifier(valor);       // efecto global
    }
}