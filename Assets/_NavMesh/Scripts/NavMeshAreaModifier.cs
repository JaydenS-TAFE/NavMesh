using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAreaModifier : MonoBehaviour
{
    private NavMeshAgent _agent;
    [System.Flags] private enum Flags
    {
        nothing,
        ground,
        grass,
        uhhhhhh,
    }
    [SerializeField] private Flags _flags;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _agent.SamplePathPosition(-1, 0f, out NavMeshHit hit);

        int filtered = hit.mask & (int)_flags;

        bool touchingMask = filtered != 0;
    }
}
