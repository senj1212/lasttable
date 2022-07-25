using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barsControl : MonoBehaviour
{
    [SerializeField] private bool isLvlBar, isTimeBar;
    [SerializeField] private GameObject gameManager;
    private RectTransform barRect;
    private int startTime;
    private float shift;
    private bool timer = true;
    private int scaler = 20;
    private bool pause = false;
    private bool start = false;

    private void Start()
    {
        barRect = gameObject.GetComponent<RectTransform>();
        SetLvlBars(GlobalValue.lvl_bar_value);
        SetTimer(GlobalValue.start_time_bar);
    }

    private void FixedUpdate()
    {
        
    }

    private void SetLvlBars(float lvl)
    {
        if (!isLvlBar)
            return;
        barRect.sizeDelta = new Vector2((barRect.sizeDelta.x / 100) * lvl, barRect.sizeDelta.y);
    }

    private void SetTimer(float startTimeValue)
    {
        if (!isTimeBar)
            return;
        startTime = (int)startTimeValue * scaler;
        shift = barRect.sizeDelta.x / startTime;
       
    }

    public void StartTimeBar()
    {
        StartCoroutine(dimOfTime());
    }

    public void SetPause(bool value)
    {
        pause = value;
    }

    IEnumerator dimOfTime()
    {
        while (timer)
        {
            if (!pause)
            {
                barRect.sizeDelta = new Vector2(barRect.sizeDelta.x - shift, barRect.sizeDelta.y);
                --startTime;
                if (startTime <= 0 || barRect.sizeDelta.x <= 0)
                {
                    TimeFinished();
                }
            }
            yield return new WaitForSeconds(1f / scaler);
        }
    }

    private void TimeFinished()
    {
        timer = false;
        barRect.sizeDelta = new Vector2(0, barRect.sizeDelta.y);
        startTime = 0;
        gameManager.GetComponent<GameControl>().DefeatedGame();
    }

    
}
