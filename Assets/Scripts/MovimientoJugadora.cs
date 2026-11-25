using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugadora : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference moveAction;   // Para arrastrar el Player/Move

    [Header("Movimiento")]
    public float velocidad = 4f;              // metros por segundo al avanzar, en la entrega final cambiará según el estado
    public float velocidadGiro = 120f;        // son los grados por segundo al girar

    CharacterController cc;
    float velocidadVertical;

    void OnEnable()  { if (moveAction != null) moveAction.action.Enable(); }
    void OnDisable() { if (moveAction != null) moveAction.action.Disable(); }

    void Start() { cc = GetComponent<CharacterController>(); }

    void Update()
    {
        if (moveAction == null) return;
        Vector2 input = moveAction.action.ReadValue<Vector2>();   // x = A/D, y = W/S

        // A/D gira, W/S avanza o retrocede
        transform.Rotate(0, input.x * velocidadGiro * Time.deltaTime, 0);

        // gravedad sencilla. Si toca el suelo (isGrounded), la velocidad vertical es −1, un empujoncito hacia abajo. Si está en el aire, cada frame se le resta la gravedad (9,8 m/s²).
        velocidadVertical = cc.isGrounded ? -1f : velocidadVertical - 9.8f * Time.deltaTime;

        Vector3 movimiento = transform.forward * input.y * velocidad + Vector3.up * velocidadVertical; // transform.forward: hacia delante según hacia dónde mira el personaje.
        cc.Move(movimiento * Time.deltaTime);
    }
}