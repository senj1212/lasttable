using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    public static GameManager instace { get; private set; }

    private Camera mainCamera;

    private void Awake()
    {
        if (instace == null)
        {
            instace = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public Vector3 GetMousePositionAtWorld(bool return_y_zero = true)
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        Vector3 mousePos = Input.mousePosition;
        Vector3 mousePositionInWorld = mainCamera.ScreenToWorldPoint(mousePos);
        return new Vector3(mousePositionInWorld.x, mousePositionInWorld.y, 0);
    }

}
