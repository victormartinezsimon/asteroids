using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        public float timeActivation;//time when this level is valid
        public float timeBetweenLastAsteroid;
        public float velocityMin;//for random values
        public float velocityMax;//for random values
        public float specialAsteroidPercent;
    }

    public Levels[] levels;
    public PoolManager normalPool;
    public PoolManager specialPool;
    public int totalLives = 5;
    public float totalTime = 60;

    [Header("UI parameters")]
    public GameObject UIOnPlay;
    public GameObject UIDamage;
    public Text UILivesText;
    public Text UITimeText;
    public Text UIScoreText;
    public GameObject UIOnStandby;
    public Text UIFinalScore;

    private bool _isPlayingGame = false;
    private int _actualLevel = 0;
    private float _lastAsteroidTime = 0;

    private Vector2 _bottomLeft;
    private Vector2 _topRight;

    private int _actualLives;
    private float _actualTimePlay;
    private int _actualScore;

    private static string PLAYER_PREFS_KEY = "BEST_SCORE";

    // Use this for initialization
    void Awake()
    {
        _instance = this;
        Random.InitState((int)System.DateTime.Now.Ticks);

        _bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        _topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        UIFinalScore.text = "Best SCORE: " + PlayerPrefs.GetFloat(PLAYER_PREFS_KEY);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        if (_isPlayingGame)
        {
            UpdateAsteroids(Time.deltaTime);
        }
    }
    /// <summary>
    /// Generate the asteroids
    /// </summary>
    /// <param name="time"></param>
    void UpdateAsteroids(float time)
    {
        _actualTimePlay += time;

        if(_actualTimePlay > totalTime)
        {
            GameOver();
            return;
        }

        if(_lastAsteroidTime + levels[_actualLevel].timeBetweenLastAsteroid <= _actualTimePlay)
        {
            //get a random asteroid
            GameObject asteroid = getRandomAsteroid(levels[_actualLevel].specialAsteroidPercent);
            
            //set the random velocity
            Movement mv = asteroid.GetComponent<Movement>();
            mv.Velocity = Random.Range(levels[_actualLevel].velocityMin, levels[_actualLevel].velocityMax);

            //get size of the asteroid
            Vector3 asteroidSize = asteroid.GetComponent<Renderer>().bounds.size;

            //set the original position
            Vector3 initialPosition = Vector3.zero;
            initialPosition.x = Random.Range(_bottomLeft.x + asteroidSize.x, _topRight.x - asteroidSize.x);
            initialPosition.y = _topRight.y + asteroidSize.y;
            asteroid.transform.localPosition = initialPosition; 

            //set when the user is damaged
            Damage d = asteroid.GetComponent<Damage>();
            d.LimitY = _bottomLeft.y;
            _lastAsteroidTime = _actualTimePlay;
        }

        //we check that there is more levels and the time is upper
        while(_actualLevel < levels.Length && _actualTimePlay > levels[_actualLevel + 1].timeActivation)
        {
            ++_actualLevel;
        }
    }

    /// <summary>
    /// prints and updates the ui
    /// </summary>
    void UpdateUI()
    {
        if(_isPlayingGame)
        {
            UITimeText.text = (totalTime - _actualTimePlay).ToString("F2");//format 00.00
            UILivesText.text = "Lives: " + _actualLives;
            UIScoreText.text = _actualScore.ToString();
        }
    }

    /// <summary>
    /// Damages the user
    /// </summary>
    public void Damage()
    {
        --_actualLives;
        UIDamage.SetActive(true);
        if(_actualLives <= 0)
        {
            GameOver();
        }
    }

    /// <summary>
    /// An asteroid is destroyed
    /// </summary>
    public void AsteroidDestroyed()
    {
        _actualScore++;
    }

    /// <summary>
    /// Return an asteroid from the pools
    /// </summary>
    /// <param name="percent"></param>
    /// <returns></returns>
    public GameObject getRandomAsteroid(float percent)
    {
        if(Random.value < percent)
        {
            return specialPool.getObject();
        }
        else
        {
            return normalPool.getObject();
        }
    }

    /// <summary>
    /// return a normal asteroid. If there is no velocity set, we get a random value
    /// </summary>
    /// <returns></returns>
    public void createANormalAsteroid(Vector3 position, float velocity = 0)
    {
        GameObject asteroid = normalPool.getObject();

        Movement mv = asteroid.GetComponent<Movement>();
        if(velocity == 0)
        {
            mv.Velocity = Random.Range(levels[_actualLevel].velocityMin, levels[_actualLevel].velocityMax);
        }
        else
        {
            mv.Velocity = velocity;
        }

        asteroid.transform.position = position;

        //set when the user is damaged
        Damage d = asteroid.GetComponent<Damage>();
        d.LimitY = _bottomLeft.y;
        _lastAsteroidTime = _actualTimePlay;
    }

    /// <summary>
    /// Returns an asteroid to the correct pool
    /// </summary>
    /// <param name="go"></param>
    public void releaseAsteroid(GameObject go)
    {
        normalPool.releaseObject(go);
        specialPool.releaseObject(go);
    }

    /// <summary>
    /// restarts a game
    /// </summary>
    public void resetGame()
    {
        _actualTimePlay = 0;
        _actualLives = totalLives;
        _isPlayingGame = true;
        _actualScore = 0;
        _actualLevel = 0;
        _lastAsteroidTime = -levels[_actualLevel].timeBetweenLastAsteroid;//setting this time, a asteroid will be created at time 0

        UIOnStandby.SetActive(false);
        UIOnPlay.SetActive(true);
    }

    /// <summary>
    /// call it when the game is finished
    /// </summary>
    public void GameOver()
    {
        _isPlayingGame = false;
        UIOnStandby.SetActive(true);
        UIOnPlay.SetActive(false);

        float best = PlayerPrefs.GetFloat(PLAYER_PREFS_KEY);
        best = Mathf.Max(best, _actualScore);
        PlayerPrefs.SetFloat(PLAYER_PREFS_KEY, best);

        UIFinalScore.text = "YOUR SCORE: " + _actualScore + "\n" + "Best SCORE: " + best;
    }
}
