using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NController : MonoBehaviour
{
    public LayerMask groundLayer; // Capa para detectar el suelo
    public NavMeshAgent agent;
    public bool isSelected = false;

    void Update()
    {
        // Manejar la seleccion del personaje
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                if (hit.transform.CompareTag("Agente")) // Cambiado a "Agente"
                {
                    // Desseleccionar todos los personajes
              

                    // Seleccionar el personaje actual y asignar su NavMeshAgent
                    isSelected = true;
                    agent = hit.transform.GetComponent<NavMeshAgent>();
                }
                else if (hit.transform.CompareTag("Ground"))
                {
                    // Desseleccionar todos los personajes si se hace clic en el suelo
                
                
                    // Asignar destino solo si no hay personajes seleccionados
                    agent.SetDestination(hit.point);
                }
            }
        }

        // Manejar el movimiento del personaje seleccionado
        if (isSelected && Input.GetMouseButtonDown(1)) // Deseleccionar con clic derecho
        {
            // Deseleccionar el personaje actual
            isSelected = false;
            // Limpiar la variable NavMeshAgent
            agent = null;
        }

        // Manejar el movimiento del personaje seleccionado
        if (isSelected && Input.GetMouseButton(0)) // Mover solo cuando est� seleccionado y se hace clic derecho
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                if (hit.transform.CompareTag("Ground"))
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
    }

    // M�todo para deseleccionar todos los personajes

}
