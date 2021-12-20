using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    private Vector2 _startPosition;
    private float _startZ;
    private Vector2 _travel => (Vector2)cam.transform.position - _startPosition;
    private float _distanceFromSubject => transform.position.z - subject.position.z;
    private float _clippingPlane => (cam.transform.position.z + (_distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));
    private float _parallaxFactor => Mathf.Abs(_distanceFromSubject) / _clippingPlane;

    public Camera cam;
    public Transform subject;

    void Start()
    {
        _startPosition = transform.position;
        _startZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = _startPosition + _travel * _parallaxFactor;
        transform.position = new Vector3(newPosition.x,newPosition.y,_startZ);
    }
}
