using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float _velocity;
    public Vector3 _direction;

    public float Velocity
    {
        get { return _velocity; }
        set { _velocity = value; }
    }

    public Vector3 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += _direction * _velocity * Time.deltaTime;
    }
}
