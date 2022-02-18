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
    // Start is called before the first frame update

    [SerializeField] float _speed = 0;
    public float speed
    {
        get => _speed;
        set => _speed = value > 0? value: 0;
    }
    // 0~360, 0 is right, increasing counterclockwise
    public float angle
    {
        get => _transform.eulerAngles.z;
        set => _transform.Rotate(0, 0, value-_transform.eulerAngles.z);
    }
    public Vector2 direction => new Vector2(speed * Mathf.Cos(angle * Mathf.PI / 180), speed * Mathf.Sin(angle * Mathf.PI / 180)); // speed为0时，direction也为0
    public float mass = 10f;
    public float resistance => 0.1f * speed * speed;

    Transform _transform;

    void Start()
    {
        _transform = transform;
    }

    public void Move()
    {
        _transform.Translate((Vector3)(direction * Time.deltaTime), Space.World);
    }
}
