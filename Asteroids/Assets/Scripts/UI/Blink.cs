using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    public float _totalTimeActive = 1;
    public float _velocityBlink = 0.3f;
    public float maxAlpha = 100;
    private Image _myImage;
    private float _timeAcumActive = 0;
    private float _timeAcumForBlink = 0;

    // Use this for initialization
    void Start()
    {
        _myImage = this.gameObject.GetComponent<Image>();
        _timeAcumActive = 0;
        _timeAcumForBlink = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _timeAcumActive += Time.deltaTime;
        _timeAcumForBlink += Time.deltaTime * _velocityBlink;

        float alphaColor = Mathf.PingPong(_timeAcumForBlink, maxAlpha);
        alphaColor = alphaColor / 255f;

        Color c = _myImage.color;
        c.a = alphaColor;
        _myImage.color = c;

        if(_timeAcumActive > _totalTimeActive)
        {
            _timeAcumActive = 0;
            _timeAcumForBlink = 0;
            this.gameObject.SetActive(false);
        }

    }
}
