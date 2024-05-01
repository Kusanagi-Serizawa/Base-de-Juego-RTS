using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using System.Collections.Generic;

public class Controller: MonoBehaviour
{
    public RectTransform selectionBox;
    public LayerMask agentLayerMask;
    public List<NavMeshAgent> selectedAgents = new List<NavMeshAgent>();

    private Vector2 startPos;
    private bool isSelecting;

    void Update()
    {
        // Detectar clic derecho
        if (Input.GetMouseButtonDown(1))
        {
            // Guardar posición inicial del clic derecho
            startPos = Input.mousePosition;
            isSelecting = true;
        }
        // Si se mantiene presionado el clic derecho
        else if (Input.GetMouseButton(1))
        {
            // Actualizar tamaño y posición del recuadro de selección
            if (isSelecting)
            {
                Vector2 currentMousePosition = Input.mousePosition;
                Vector2 boxStart = startPos;
                Vector2 boxEnd = currentMousePosition;
                selectionBox.gameObject.SetActive(true);
                selectionBox.position = boxStart;
                selectionBox.sizeDelta = boxEnd - boxStart;
            }
        }
        // Si se suelta el clic derecho
        else if (Input.GetMouseButtonUp(1))
        {
            // Ocultar el recuadro de selección
            selectionBox.gameObject.SetActive(false);
            isSelecting = false;

            // Realizar la selección de agentes dentro del recuadro
            SelectAgentsInBox(startPos, Input.mousePosition);
        }

        // Si se hace clic izquierdo
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            // Mover todos los agentes seleccionados hacia el punto donde se hizo clic
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, agentLayerMask))
            {
                foreach (NavMeshAgent agent in selectedAgents)
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
    }

    void SelectAgentsInBox(Vector2 startPos, Vector2 endPos)
    {
        selectedAgents.Clear(); // Limpiar la lista de agentes seleccionados

        // Obtener todos los objetos con el componente NavMeshAgent
        NavMeshAgent[] allAgents = FindObjectsOfType<NavMeshAgent>();

        // Convertir las coordenadas de la pantalla a rayos en el mundo
        Rect selectionRect = new Rect(startPos, endPos - startPos);
        foreach (NavMeshAgent agent in allAgents)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(agent.transform.position);
            if (selectionRect.Contains(screenPosition))
            {
                // Si el objeto está dentro del recuadro, agregar su componente NavMeshAgent a la lista de agentes seleccionados
                selectedAgents.Add(agent);
            }
        }
    }
}
