using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private float _y;

    public float LimitY
    {
        set { _y = value; }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition.y < _y)
        {
            GameManager.Instance().Damage();
            GameManager.Instance().releaseAsteroid(this.gameObject);
        }
    }
}
