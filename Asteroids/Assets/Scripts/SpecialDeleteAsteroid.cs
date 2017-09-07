using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialDeleteAsteroid : DeleteAsteroid
{

    public override void AsteroidHit()
    {
        //create the 2 asteroids
        GameObject a1 = GameManager.Instance().getNormalAsteroid();
        Vector3 newPos1 = this.transform.position;
        newPos1.x += a1.GetComponent<Renderer>().bounds.size.x / 2;
        this.transform.position = newPos1;

        GameObject a2 = GameManager.Instance().getNormalAsteroid();
        Vector3 newPos2 = this.transform.position;
        newPos2.x += a2.GetComponent<Renderer>().bounds.size.x / 2;
        this.transform.position = newPos2;

        base.AsteroidHit();//call the parent
    }
}
