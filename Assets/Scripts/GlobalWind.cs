using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GlobalWind : MonoBehaviour
{
    static GlobalWind _instance;
    public static GlobalWind Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("GlobalWind").GetComponent<GlobalWind>();
            }

            if (_instance == null)
            {
                _instance = new GameObject("GlobalWind").AddComponent<GlobalWind>();
            }

            return _instance;
        }
    }

    // 0~360, 0 is right, increasing counterclockwise
    [Range(0, 360)]public float angle = 0;
    [Range(0, 100)] public float speed = 5;

    public Vector2 direction => new Vector2(speed * Mathf.Cos(angle * Mathf.PI / 180), speed * Mathf.Sin(angle * Mathf.PI / 180));

    void Start()
    {
    }

    void Update()
    {
    }
}
