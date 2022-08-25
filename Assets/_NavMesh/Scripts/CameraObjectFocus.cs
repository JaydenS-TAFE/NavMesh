using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObjectFocus : MonoBehaviour
{
    [SerializeField] Transform _target;
    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.localPosition;
    }

    private void Update()
    {
        transform.position = _target.position + _offset;
    }
}
