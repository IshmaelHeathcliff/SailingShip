using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBody : MonoBehaviour
{
    static ShipBody _instance;
    public static ShipBody Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("ShipBody").GetComponent<ShipBody>();
            }

            if (_instance == null)
            {
                _instance = new GameObject("ShipBody").AddComponent<ShipBody>();
            }

            return _instance;
        }
    }
    
    public Vector2 Speed => _rigidbody.velocity;

    Rigidbody2D _rigidbody;
    
    Transform _transform;

    const float ResistanceAmend = 1f;

    void Start()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rigidbody.AddForce(Vector2.Dot(Sail.Instance.SailPower, _transform.right) * _transform.right);
        _rigidbody.AddForce(-Speed * ResistanceAmend * Speed.magnitude);
    }
}
