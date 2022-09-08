using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3[] _positions;
    [SerializeField] private AnimationCurve _curve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f, 1f, 1f), new Keyframe(1f, 1f, 1f, 1f) });
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _pauseTime = 1f;
    private NavMeshSurface _navMeshSurface;
    private int _currentPositionIndex;
    private float _time;
    private Vector3 _originPoint;
    private float _pauseTimer;

    private void Awake()
    {
        _navMeshSurface = GetComponent<NavMeshSurface>();
    }

    private void Start()
    {
        _originPoint = transform.position;

        //Create navmesh links for each position
        for (int i = 0; i < _positions.Length; i++)
        {
            bool forward = true;
            for (int j = 0; j < 4; j++)
            {
                Vector3 direction = Quaternion.Euler(0, 90 * j, 0) * Vector3.forward;

                NavMeshLink link = gameObject.AddComponent<NavMeshLink>();

                Vector3 sizeDirection = direction * (forward ? _navMeshSurface.navMeshData.sourceBounds.size.z : _navMeshSurface.navMeshData.sourceBounds.size.x);

                link.startPoint = _positions[i] + ((-1f * direction) + sizeDirection * 0.5f);
                link.endPoint = _positions[i] + ((1f * direction) + (sizeDirection * 0.5f));

                link.width = !forward ? _navMeshSurface.navMeshData.sourceBounds.size.z : _navMeshSurface.navMeshData.sourceBounds.size.x;

                forward = !forward;
            }
        }
    }

    private void Update()
    {
        if (_pauseTimer > 0f)
        {
            _pauseTimer -= Time.deltaTime;
            return;
        }

        _time += _speed * Time.deltaTime;
        transform.position = Vector3.Lerp(_originPoint + _positions[_currentPositionIndex], _originPoint + _positions[(_currentPositionIndex + 1) % _positions.Length], _curve.Evaluate(_time));
        if (_time >= 1f)
        {
            _time = 0f;
            _pauseTimer = _pauseTime;
            _currentPositionIndex = (_currentPositionIndex + 1) % _positions.Length;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_positions.Length == 0)
            return;

        Vector3 origin = Application.isPlaying ? _originPoint : transform.position;

        Gizmos.color = Color.red;
        for (int i = 0; i < _positions.Length; i++)
        {
            Gizmos.DrawLine(origin + _positions[i], origin + _positions[(i + 1) % _positions.Length]);
        }
        for (int i = 0; i < _positions.Length; i++)
        {
            Gizmos.DrawSphere(origin + _positions[i], 0.25f);
        }

    }
}
