using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshPathVisualiserGizmo : MonoBehaviour
{
    [SerializeField] private bool _alwaysShow = false;

    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnDrawGizmosSelected()
    {
        if (_alwaysShow)
            return;
        Draw();
    }
    private void OnDrawGizmos()
    {
        if (!_alwaysShow)
            return;
        Draw();
    }

    private void Draw()
    {
        if (_agent == null)
            return;

        Gizmos.color = Color.red;
        for (int i = 0; i < _agent.path.corners.Length - 1; i++)
        {
            Gizmos.DrawLine(_agent.path.corners[i], _agent.path.corners[i + 1]);
        }
        for (int i = 0; i < _agent.path.corners.Length - 1; i++)
        {
            Gizmos.DrawSphere(_agent.path.corners[i], 0.1f);
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_agent.destination, 0.25f);
    }
}
