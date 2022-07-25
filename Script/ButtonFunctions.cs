using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("game");
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    public void AdsBtn()
    {

    }

    public void SettingsBtn()
    {

    }

    public void ShopBtn()
    {

    }

    public void MenuBtn()
    {
        SceneManager.LoadScene("menu");
    }

    public void PauseBtn()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameControl>().SetPause();
    }

    public void ButtonGo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
