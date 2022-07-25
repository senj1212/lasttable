using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    private GameObject shopPanel;

    private void Awake()
    {
        shopPanel = GameObject.FindGameObjectWithTag("shop");
    }

}
