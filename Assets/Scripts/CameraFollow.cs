using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ship;

    Transform _shipTransform;
    Transform _transform;
    
    void Start()
    {
        _transform = transform;
        _shipTransform = ship.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = _shipTransform.position - _transform.position;
        moveDirection.z = 0;
        _transform.Translate(moveDirection);
    }
}
