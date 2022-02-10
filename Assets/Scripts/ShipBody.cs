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

    public float speed = 0;
    // 0~360, 0 is right, increasing counterclockwise
    public float angle = 0;
    public Vector2 direction = Vector2.right;
    public float mass = 10f;
    public float resistance = 0;

    Transform m_Transform;

    public void UpdateDirection()
    {
        if (speed < 0) speed = 0;
        direction = new Vector2(speed * Mathf.Cos(angle * Mathf.PI / 180), 
            speed * Mathf.Sin(angle * Mathf.PI / 180));
        if(Math.Abs(m_Transform.eulerAngles.z - angle) > Mathf.Epsilon)
            m_Transform.Rotate(0, 0, angle-m_Transform.eulerAngles.z);
    }

    public void UpdateResistance()
    {
        resistance = 0.1f * speed * speed;
    }
    
    void Start()
    {
        m_Transform = transform;
        UpdateDirection();
    }

    public void Move()
    {
        m_Transform.Translate(direction * Time.deltaTime);
    }
}
