using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float _y;

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
            PoolManager.Instance().releaseObject(this.gameObject);
        }
    }
}
