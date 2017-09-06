using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager Instance()
    {
        return _instance;
    }

    [System.Serializable]
    public struct Levels
    {
        public string name; //its only because the inspector will use this
        public float timeActivation;
        public float timeBetweenLastAsteroid;
        public float velocityMin;
        public float velocityMax;
    }

    public Levels[] _levels;

    public float _timePlay;

	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateUI();
        UpdateAsteroids();
	}

    void UpdateUI()
    {

    }

    public void Damage()
    {

    }

    void UpdateAsteroids()
    {

    }

    public void AsteroidDestroyed()
    {

    }
}
