using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialClick : Click
{
    public override void Action()
    {
        //create the 2 asteroids
        Vector3 newPos1 = this.transform.position;
        newPos1.x += this.GetComponent<Renderer>().bounds.size.x / 2;
        GameManager.Instance().createANormalAsteroid(newPos1);

        Vector3 newPos2 = this.transform.position;
        newPos2.x -= this.GetComponent<Renderer>().bounds.size.x / 2;
        GameManager.Instance().createANormalAsteroid(newPos2, this.gameObject.GetComponent<Movement>().Velocity);

        //delete this gameobject
        GameManager.Instance().releaseAsteroid(this.gameObject);
    }
}
