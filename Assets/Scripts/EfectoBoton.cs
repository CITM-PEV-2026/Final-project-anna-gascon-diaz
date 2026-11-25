using UnityEngine;

public class EfectoBoton : MonoBehaviour
{
    public float hundimiento = 0.05f;
    bool pulsado = false;

    public void Pulsar()
    {
        if (pulsado) return;
        pulsado = true;
        transform.localPosition += Vector3.down * hundimiento;   // el botón se hunde
        GetComponent<Renderer>().material.color = Color.green;   // y se pone verde
    }
}