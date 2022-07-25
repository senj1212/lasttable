using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Starter : MonoBehaviour
{
    [SerializeField] private List<Elements> allElements = new List<Elements>();

    private void Awake()
    {
        GlobalValue.allElements = allElements;
    }
}
