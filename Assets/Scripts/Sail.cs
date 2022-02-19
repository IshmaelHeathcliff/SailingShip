using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sail : MonoBehaviour
{
    static Sail _instance;
    public static Sail Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Sail").GetComponent<Sail>();
            }

            if (_instance == null)
            {
                GameObject shipBody = GameObject.Find("ShipBody");
                var sail = new GameObject("Sail");
                sail.transform.SetParent(shipBody.transform);
                _instance = sail.AddComponent<Sail>();
            }

            return _instance;
        }
    }
    
    Transform _transform;
    
    [Range(0,2f)]public float radian = 1f;
    public float area = 5f;

    public Vector2 SailPower { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        var direction = (Vector2)_transform.right;

        // 0~360, 0 is right, increasing counterclockwise
        var globalWindDirection = GlobalWind.Instance.Speed;
        var shipSpeed = ShipBody.Instance.Speed;
        var windSpeed = globalWindDirection - shipSpeed;

        if (Vector2.Dot(direction, globalWindDirection) < 0)
        {
            _transform.Rotate(0, 0, 180);
        }



        var liftWindSpeed = Vector2.Dot(windSpeed ,_transform.up);
        var resistanceWindSpeed = Vector2.Dot(windSpeed, direction);

        var lift = radian * Mathf.Abs(liftWindSpeed) * area;
        var resistance = resistanceWindSpeed * area;
        SailPower = (lift + resistance) * direction;
    }
}
