using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    float time;
    private TMP_Text text;
    bool timerStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStarted)
        {
            if (time > 0) time -= Time.deltaTime;
            else time = 0;
            DisplayTime();
        }
    }

    public void StartTimer(float _time)
    {
        time = _time;
        timerStarted = true;
    }

    private void DisplayTime()
    {
        int _seconds;
        int _minutes;
        int _hours;

        string _s;
        string _m;
        string _h;

        _hours = (int)time / 3600;
        _minutes = ((int)time % 3600) / 60;
        _seconds = (int)time % 60;

        _h = _hours.ToString();

        if(_minutes < 10)
        {
            _m = "0" + _minutes;
        }
        else
        {
            _m = _minutes.ToString();
        }

        if (_seconds < 10)
        {
            _s = "0" + _seconds;
        }
        else
        {
            _s = _seconds.ToString();
        }

        if (_hours > 0)
        {
            text.text = _h + ":" + _m + ":" + _s;
        }
        else
        {
            text.text = _m + ":" + _s;
        }
        
    }

}
