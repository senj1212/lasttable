using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Camera cameraMain;

    private void Start()
    {
        cameraMain = Camera.main;
    }

    public GameObject CheckMouseUpGameObject()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(cameraMain.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (rayHit.transform != null)
            return rayHit.collider.gameObject;
        return null;
    }
}
