using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MovimientoJugadora : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference moveAction;   // Player/Move

    [Header("Movimiento")]
    public float velocidad = 4f;              // en la entrega final cambiará según el estado
    public float velocidadGiro = 120f;

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

        // gravedad sencilla
        velocidadVertical = cc.isGrounded ? -1f : velocidadVertical - 9.8f * Time.deltaTime;

        Vector3 movimiento = transform.forward * input.y * velocidad + Vector3.up * velocidadVertical;
        cc.Move(movimiento * Time.deltaTime);
    }
}