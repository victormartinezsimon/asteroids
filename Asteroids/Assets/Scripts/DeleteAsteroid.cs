using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAsteroid : MonoBehaviour {

	public virtual void AsteroidHit()
    {
        GameManager.Instance().releaseAsteroid(this.gameObject);
    }
}
