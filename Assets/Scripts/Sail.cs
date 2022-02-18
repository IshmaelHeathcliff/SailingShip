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
    
    [Range(1,1.5f)]public float radian = 1.1f;
    public float area = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        // 0~360, 0 is right, increasing counterclockwise
        var sailAngle = _transform.eulerAngles.z;
        var sailLocalAngle = _transform.localEulerAngles.z;
        var globalWindDirection = GlobalWind.Instance.direction;
        var shipDirection = ShipBody.Instance.direction;
        var windDirection = globalWindDirection - shipDirection;

        float windAngle;
        if (windDirection.x == 0)
            windAngle = windDirection.y != 0 ? 90f : 0f;
        else
            windAngle = Mathf.Atan(windDirection.y / windDirection.x) * 180 / Mathf.PI;

        var relativeAngle = windAngle - sailAngle;
        var windSpeed = windDirection.magnitude;

        var liftWindSpeed = windSpeed * Mathf.Abs(Mathf.Sin(relativeAngle * Mathf.PI / 180));
        var resistanceWindSpeed = windSpeed * Mathf.Cos(relativeAngle * Mathf.PI / 180);

        var lift = (radian * radian - 1f) * liftWindSpeed * area;
        var resistance = resistanceWindSpeed * area;
        var sailPower = lift + resistance;
        // Debug.Log($"windSpeed: {windSpeed}, lift: {lift}, resistance: {resistance}, sailPower: {sailPower}");
        // Debug.Log($"relativeAngle: {relativeAngle}, sailAngle: {sailAngle}, windAngle: {windAngle}");
        // Debug.Log($"{ShipBody.Instance.direction}");
        // Debug.Log($"{ShipBody.Instance.speed}");

        var forwardPower = sailPower * Mathf.Cos(sailLocalAngle * Mathf.PI / 180);

        ShipBody.Instance.speed += (forwardPower - ShipBody.Instance.resistance) * Time.deltaTime;
        ShipBody.Instance.Move();
    }
}
