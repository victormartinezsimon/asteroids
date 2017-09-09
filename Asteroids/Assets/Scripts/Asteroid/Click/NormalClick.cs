using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalClick : Click
{
    public override void Action()
    {
        GameManager.Instance().releaseAsteroid(this.gameObject);
    }
}
