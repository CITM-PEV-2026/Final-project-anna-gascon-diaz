using UnityEngine;

public class EfectoBoton : MonoBehaviour
{
    public float hundimiento = 0.05f; //cuánto baja el botón, en metros.
    bool pulsado = false;

    public void Pulsar() //función que se conecta en "Al Interactuar".
    {
        if (pulsado) return; //Si ya estaba pulsado, no hace nada. Si no, lo marca como pulsado. Así el botón solo se hunde una vez.
        pulsado = true;
        transform.localPosition += Vector3.down * hundimiento;   // el botón se hunde
        GetComponent<Renderer>().material.color = Color.green;   // y se pone verde
    }
}