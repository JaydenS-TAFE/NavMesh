using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RandomMove : MonoBehaviour
{
    //[SerializeField] private Camera _camera;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetRandomDestination();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _agent.destination) <= _agent.stoppingDistance * 2f)
        {
            SetRandomDestination();
        }

        //_camera.transform.forward = _agent.destination - _camera.transform.position;
        //_camera.transform.localEulerAngles = new Vector3(_camera.transform.localEulerAngles.x, 0f, _camera.transform.localEulerAngles.z);
    }

    private void SetRandomDestination()
    {
        //Vector3 bounds = _navMesh.navMeshData.sourceBounds.extents;
        Vector3 bounds = GameManager.instance.bounds.extents;
        Vector3 random = new Vector3(Random.Range(-bounds.x, bounds.x), Random.Range(-bounds.y, bounds.y), Random.Range(-bounds.z, bounds.z));
        //_agent.destination = _navMesh.navMeshData.sourceBounds.center + random;
        _agent.destination = GameManager.instance.bounds.center + random;
    }
}
