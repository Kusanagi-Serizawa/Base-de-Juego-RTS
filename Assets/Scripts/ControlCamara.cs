using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamara : MonoBehaviour
{
    public float velocidadMovimiento = 6.0f; // Velocidad de movimiento de la cámara
    public float velocidadRotacion = 5f; // Velocidad de rotación de la cámara
    public float velocidadArrastre = 1f; // Velocidad de arrastre del mouse

    private Vector3 inicioArrastre; // Posición inicial del arrastre
    private bool arrastrando = false; // Indicador de si se está arrastrando

    void Update()
    {
        // Movimiento horizontal y vertical de la cámara
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        // Calculamos el desplazamiento de la cámara
        Vector3 movement = new Vector3(horizontalMovement, 0.0f, verticalMovement) * velocidadMovimiento * Time.deltaTime;

        // Aplicamos el desplazamiento a la posición actual de la cámara
        transform.Translate(movement, Space.World);

        // Rotación con el scroll del mouse
        if (Input.GetMouseButtonDown(2)) // 2 representa el botón del scroll del mouse
        {
            float rotacionMouse = Input.GetAxis("Mouse X") * velocidadRotacion;
            transform.Rotate(Vector3.up, rotacionMouse);
        }

        // Arrastre del mouse para rotación
        if (Input.GetMouseButtonDown(0))
        {
            inicioArrastre = Input.mousePosition;
            arrastrando = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            arrastrando = false;
        }

        if (arrastrando)
        {
            Vector3 direccionArrastre = Input.mousePosition - inicioArrastre;
            float rotacionY = direccionArrastre.x * velocidadArrastre * Time.deltaTime;

            transform.Rotate(Vector3.up, rotacionY);

            inicioArrastre = Input.mousePosition;
        }
    }
}
