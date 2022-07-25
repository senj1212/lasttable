using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    [SerializeField] private GameObject prefMenuNonStart, prefMenuIsStart;
    [SerializeField] private RectTransform imgMenu;
    private bool activMenu = false;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.allCameras[0];
    }

    private void Update()
    {
        if (activMenu && Input.GetMouseButtonDown(0))
        {
            var gmClick = mainCamera.GetComponent<CameraControl>().CheckMouseUpGameObject();
            if (gmClick == null || gmClick.tag != "menuBTN")
            {
                HideMenu();
            }
            else
            {

            }
            
        }
    }

    private void OnMouseUpAsButton()
    {
        if (!activMenu)
            ShowMenu();
        else
            HideMenu();
    }

    private void ShowMenu()
    {
        activMenu = true;
        if (GlobalValue.gameStart)
        {
            prefMenuIsStart.SetActive(activMenu);
        }
        else
        {
            prefMenuNonStart.SetActive(activMenu);
        }
        imgMenu.rotation = Quaternion.Euler(0, 0, 90);
    }

    private void HideMenu()
    {
        activMenu = false;
        if (GlobalValue.gameStart)
        {
            prefMenuIsStart.SetActive(activMenu);
        }
        else
        {
            prefMenuNonStart.SetActive(activMenu);
        }
        imgMenu.rotation = Quaternion.Euler(0, 0, -90);
    }
}
