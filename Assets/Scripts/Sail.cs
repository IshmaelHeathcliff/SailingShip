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
    
    [Range(1,2)]public float radian = 1.1f;
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
        var shipResistance = ShipBody.Instance.resistance;
        Vector2 globalWindDirection = GlobalWind.Instance.direction;
        Vector2 shipDirection = ShipBody.Instance.direction;
        Vector2 windDirection = globalWindDirection - shipDirection;

        float windAngle;
        if (windDirection.x == 0)
        {
            windAngle = windDirection.y != 0 ? 90f : 0f;
        }
        else
        {
            windAngle = Mathf.Atan(windDirection.y / windDirection.x);
        }
        
        var relativeAngle = windAngle - sailAngle;
        var windSpeed = windDirection.magnitude;

        var liftWindSpeed = windSpeed * Mathf.Abs(Mathf.Sin(relativeAngle * Mathf.PI / 180));
        var resistanceWindSpeed = windSpeed * Mathf.Cos(relativeAngle * Mathf.PI / 180);

        var lift = 0.1f * (radian * radian - 1f) * liftWindSpeed * liftWindSpeed * area;
        var resistance = resistanceWindSpeed * area;
        var sailPower = lift + resistance;

        var forwardPower = sailPower * Mathf.Cos(sailLocalAngle * Mathf.PI / 180);

        ShipBody.Instance.speed += 0.1f * (forwardPower - shipResistance);
        var shipSpeed = ShipBody.Instance.speed;
        ShipBody.Instance.resistance = 0.1f * shipSpeed * shipSpeed;
        
        ShipBody.Instance.Move();
    }
}
