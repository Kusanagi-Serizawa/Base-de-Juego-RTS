using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seleccionador : MonoBehaviour
{
    public RectTransform selectionBox;
    public LayerMask agentLayerMask;

    private List<UnityEngine.AI.NavMeshAgent> selectedAgents = new List<UnityEngine.AI.NavMeshAgent>();
    private Vector2 startPos;
    private bool isSelecting;

    void Update()
    {
        // Manejar la selección de los personajes con clic izquierdo
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, agentLayerMask))
            {
                UnityEngine.AI.NavMeshAgent agent = hit.transform.GetComponent<UnityEngine.AI.NavMeshAgent>();

                if (agent != null)
                {
                    if (!selectedAgents.Contains(agent))
                    {
                        DeseleccionarTodosAgentes();
                        SeleccionarAgente(agent);
                    }
                    else
                    {
                        DeseleccionarAgente(agent);
                    }
                }
                else
                {
                    DeseleccionarTodosAgentes();
                }
            }
        }

        // Manejar la selección de múltiples personajes con selección de caja
        if (Input.GetMouseButtonDown(1))
        {
            startPos = Input.mousePosition;
            isSelecting = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            if (isSelecting)
            {
                isSelecting = false;
                selectionBox.gameObject.SetActive(false);
                RealizarSeleccionEnCaja(startPos, Input.mousePosition);
            }
        }

        if (isSelecting)
        {
            Vector2 currentMousePosition = Input.mousePosition;
            Vector2 boxStart = startPos;
            Vector2 boxEnd = currentMousePosition;
            selectionBox.gameObject.SetActive(true);
            selectionBox.position = boxStart;
            selectionBox.sizeDelta = boxEnd - boxStart;
        }

        // Manejar el movimiento de los personajes seleccionados con clic derecho
        if (Input.GetMouseButtonDown(1) && selectedAgents.Count > 0 && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, -1))
            {
                foreach (UnityEngine.AI.NavMeshAgent agent in selectedAgents)
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
    }

    void SeleccionarAgente(UnityEngine.AI.NavMeshAgent agent)
    {
        selectedAgents.Add(agent);
    // Cambiar el color del material del agente (ejemplo)
    Renderer agentRenderer = agent.GetComponent<Renderer>();
    if (agentRenderer != null)
    {
        agentRenderer.material.color = Color.blue; // Cambia el color del agente seleccionado
    }
    }

    void DeseleccionarAgente(UnityEngine.AI.NavMeshAgent agent)
    {
        selectedAgents.Remove(agent);
    // Restaurar el color original del material del agente (ejemplo)
    Renderer agentRenderer = agent.GetComponent<Renderer>();
    if (agentRenderer != null)
    {
        agentRenderer.material.color = Color.red; // Restaura el color original del agente
    }
    }

    void DeseleccionarTodosAgentes()
    {
        foreach (UnityEngine.AI.NavMeshAgent agent in selectedAgents)
    {
        // Restaurar el color original del material del agente (ejemplo)
        Renderer agentRenderer = agent.GetComponent<Renderer>();
        if (agentRenderer != null)
        {
            agentRenderer.material.color = Color.red; // Restaura el color original del agente
        }
    }
    selectedAgents.Clear();
    }

    void RealizarSeleccionEnCaja(Vector2 boxStart, Vector2 boxEnd)
    {
        DeseleccionarTodosAgentes();
        
        Rect selectionRect = new Rect(boxStart, boxEnd - boxStart);
        Collider[] hitColliders = Physics.OverlapBox(selectionRect.center, new Vector3(selectionRect.width / 2f, 1f, selectionRect.height / 2f), Quaternion.identity, agentLayerMask);

        foreach (Collider col in hitColliders)
        {
            UnityEngine.AI.NavMeshAgent agent = col.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (agent != null)
            {
                SeleccionarAgente(agent);
            }
        }
    }
}
