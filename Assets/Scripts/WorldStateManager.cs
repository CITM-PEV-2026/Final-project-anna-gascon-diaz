using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorldStateManager : MonoBehaviour
{
    public static WorldStateManager Instance;          // para encontrarlo desde cualquier script

    [Header("Estado del mundo")]                       // Pone un título en el inspector, es solo visual
    [Range(-1f, 1f)] public float worldState = 0f;     // barra deslizante: -1 distopía, 0 neutro, +1 utopía

    [Header("Input de depuración")]                    
    public InputActionReference stateDownAction;       // Debug/StateDown (tecla 1)
    public InputActionReference stateUpAction;         // Debug/StateUp (tecla 2)

    public event Action<float> OnWorldStateChanged;    // otros scripts se suscriben aquí, todos los scripts suscritos reciben ese valor 

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }// para evitar errores. si ya existe un worldmanager lo elimina para que solo exista este
        Instance = this;                               //Avisa de que el WorldManager es este script
    }

    void OnEnable()
    {
        if (stateDownAction != null) stateDownAction.action.Enable();
        if (stateUpAction != null) stateUpAction.action.Enable();
    }

    void OnDisable()
    {
        if (stateDownAction != null) stateDownAction.action.Disable();
        if (stateUpAction != null) stateUpAction.action.Disable();
    }

    void Update()
    {
        if (stateDownAction != null && stateDownAction.action.WasPressedThisFrame()) AddModifier(-0.1f);
        if (stateUpAction != null && stateUpAction.action.WasPressedThisFrame()) AddModifier(0.1f);
    }

    // Las acciones llaman a esta función para cambiar el estado del mundo
    public void AddModifier(float valor)
    {
        float nuevo = Mathf.Clamp(worldState + valor, -1f, 1f); // Mathf.Clamp(valor, mínimo, máximo) limita un número para que nunca se salga de un rango
        if (Mathf.Approximately(nuevo, worldState)) return;   // seria como a == b pero más exacto, comprueba si ya estaba en el límite

        worldState = nuevo; 
        Debug.Log("Estado del mundo: " + worldState.ToString("0.00")); // se muestra en la consola
        OnWorldStateChanged?.Invoke(worldState);            // avisa a todos los suscritos. El ?. significa "solo si hay alguien suscrito". Sin él, daría error si la lista está vacía.
    }
}