using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad del movimiento
    public float distancia = 20f; // Distancia máxima a la que se moverá el objeto

    private bool moviendoDerecha = true; // Indicador de dirección

    void Update()
    {
        // Si el objeto está dentro del rango de movimiento
        if (transform.position.x < distancia && moviendoDerecha)
        {
            // Mover hacia la derecha
            transform.Translate(Vector3.right * velocidad * Time.deltaTime);
        }
        else
        {
            // Cambiar la dirección si se alcanza la distancia máxima
            moviendoDerecha = false;
        }

        // Si el objeto está dentro del rango de movimiento
        if (transform.position.x > -distancia && !moviendoDerecha)
        {
            // Mover hacia la izquierda
            transform.Translate(Vector3.left * velocidad * Time.deltaTime);
        }
        else
        {
            // Cambiar la dirección si se alcanza la distancia máxima
            moviendoDerecha = true;
        }
    }
}
