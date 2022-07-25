using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class Elements : MonoBehaviour
{
    [SerializeField] private string nameElemets = "none";
    [SerializeField] private int count = 10;
    public Color colorElements = Color.white;
    [SerializeField] public int id = 0;
    [SerializeField] private List<int> comparableElements = new List<int>();
    [SerializeField] private GameObject particleObject;
    [SerializeField] public Sprite img;
    

    private TextMeshProUGUI countText;
    private Camera mainCamera;
    private GameObject creatingParticle;

    private void Awake()
    {
        mainCamera = Camera.allCameras[0];
        countText = this.transform.Find("count").gameObject.GetComponent<TextMeshProUGUI>();
        UpdateText();
    }

    private void OnMouseDown()
    {
        if (!CheckCount()) return;

        if (particleObject != null)
        {
            creatingParticle = Instantiate(particleObject, mainCamera.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            ParticleSystem.MainModule settings = creatingParticle.GetComponent<ParticleSystem>().main;
            settings.startColor = colorElements;
        }
    }

    private void OnMouseUp()
    {
        if (!CheckCount()) return;

        Destroy(creatingParticle);

        GameObject currentCell = mainCamera.GetComponent<CameraControl>().CheckMouseUpGameObject();
        try
        {
            if (currentCell.GetComponent<Cell>().SetElement(this)) count--;

            UpdateText();
        }
        catch
        {

        }


    }

    private void UpdateText() => countText.text = count.ToString();

    private bool CheckCount() => count > 0 ? true : false;

    public bool IsComparable(Elements elem)
    {
        if (comparableElements.Contains(elem.id)) return true;
        return false;
    }

}
