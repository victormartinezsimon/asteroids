using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

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

    private bool _playingGame = false;
    public int actualLevel = -31;
    public float lastAsteroidTime = 0;

    private Vector2 bottomLeft;
    private Vector2 topRight;

    // Use this for initialization
    void Awake()
    {
        _instance = this;
        Random.InitState((int)System.DateTime.Now.Ticks);

        bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        if (_playingGame)
        {
            UpdateAsteroids(Time.deltaTime);
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                _playingGame = true;
            }
        }
    }
    void UpdateAsteroids(float time)
    {
        _timePlay += time;

        if(_timePlay > 6000)
        {
            //the game ended
            _playingGame = false;
            return;
        }

        if(lastAsteroidTime + _levels[actualLevel].timeBetweenLastAsteroid <= _timePlay)
        {
            //instatiate a new asteroid
            GameObject asteroid = PoolManager.Instance().getObject();
            Movement mv = asteroid.GetComponent<Movement>();
            mv.Velocity = Random.Range(_levels[actualLevel].velocityMin, _levels[actualLevel].velocityMax);

            Vector3 asteroidSize = asteroid.GetComponent<Renderer>().bounds.size;

            Vector3 initialPosition = Vector3.zero;
            initialPosition.x = Random.Range(bottomLeft.x + asteroidSize.x, topRight.x - asteroidSize.x);
            initialPosition.y = topRight.y + asteroidSize.y;
            asteroid.transform.localPosition = initialPosition; 

            Damage d = asteroid.GetComponent<Damage>();
            d.LimitY = bottomLeft.y;
            lastAsteroidTime = _timePlay;
        }

        while(actualLevel < _levels.Length && _timePlay > _levels[actualLevel + 1].timeActivation)
        {
            ++actualLevel;
        }
    }

    void UpdateUI()
    {

    }

    public void Damage()
    {

    }


    public void AsteroidDestroyed()
    {

    }
}
