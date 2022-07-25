using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    [SerializeField] private GameObject winWindow, defeatWindow, topPanel;
    private barsControl timeBar, lvlBar;
    private bool started = false;
    private bool pause = false;

    private void Start()
    {
        lvlBar = GameObject.Find("lvl_bar").GetComponent<barsControl>();
        timeBar = GameObject.Find("time_bar").GetComponent<barsControl>();
    }

    public void StartGame()
    {
        if (!started)
        {
            started = true;
            GlobalValue.gameStart = true;
        }
    }

    public void WinGame()
    {
        var o = Instantiate(winWindow, Vector3.zero, Quaternion.identity);
        topPanel.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.5f);
        
        o.transform.SetParent(topPanel.transform);
        o.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        GlobalValue.gameStart = false;
        started = false;
    }

    public void DefeatedGame()
    {
        var o = Instantiate(defeatWindow, Vector3.zero, Quaternion.identity);
        topPanel.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.5f);
        
        o.transform.SetParent(topPanel.transform);
        o.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        GlobalValue.gameStart = false;
        started = false;
    }

    public void SetPause()
    {
        pause = !pause;
        timeBar.GetComponent<barsControl>().SetPause(pause);
    }
}
