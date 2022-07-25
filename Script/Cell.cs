using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public int x, y;
    [SerializeField] private Sprite activCell, unactivCell;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private bool startedCell, finishedCell;
    private Elements currentElement = null;
    [SerializeField] private List<Cell> neighborCells = new List<Cell>();
    private bool isCellActive = false;
    private Image img;

    public void OnCreate(bool isStart = false, bool isFinish = false)
    {
        if (isStart)
        {
            startedCell = isStart;
            GlobalValue.startCell = this;
            SetElement(GlobalValue.allElements[Random.Range(0, GlobalValue.allElements.Count)]);
        }
        else if (isFinish)
        {
            finishedCell = isFinish;
            GlobalValue.finishCell = this;
            SetElement(GlobalValue.allElements[Random.Range(0, GlobalValue.allElements.Count)]);
        }

        img = this.gameObject.transform.Find("img").GetComponent<Image>();
        gameManager = GameObject.Find("GameManager");
    }

    private void OnMouseOver()
    {
        if (currentElement != null && Input.GetMouseButtonDown(1))
        {
            ClearElements();
        }
    }

    public bool SetElement(Elements newElement) 
    {
        if (currentElement != null) return false;

        ChangeActive(false);

        if (startedCell) ChangeActive(true);

        currentElement = newElement;

        img.sprite = newElement.img;

        if (!startedCell && !finishedCell)
        {
            if (!GlobalValue.gameStart)
                gameManager.GetComponent<GameControl>().StartGame();
            AllCheckCell();
        }
        return true;
    }

    public void ClearElements()
    {
        if (startedCell || finishedCell) return; 

        ChangeActive(false);
        currentElement = null;
        img.color = new Color(1f, 1f, 1f, 0f);
        img.sprite = null;

        AllCheckCell();        
    }

    public void CheckStartLink()
    {
        if (!startedCell) return;
        var nei = CheckElementsNeighbors();
        foreach (Cell c in nei)
        {
            c.CheckNextLink();
        }

    }

    public void CheckNextLink()
    {
        if (startedCell) return;

        var nei = CheckElementsNeighbors();
        ChangeActive(true);
        foreach (Cell c in nei)
        {
            if (!c.GetIsActiveCell())
                c.CheckNextLink();
        }
    }

    public Elements GetCurrentElement() => currentElement;
    public bool GetIsActiveCell() => isCellActive;
    public Vector2 GetPos() => new Vector2(x, y);
    public List<Cell> GetNeigbors() => neighborCells;

    public void FindAllNeighbors(Dictionary<Vector2, Cell> allCells)
    {
        var cellList = allCells.Values;

        foreach (Cell neighbor in cellList)
        {
            if (y % 2 != 0)
            {
                if (((x == neighbor.x || x == neighbor.x - 1) && (y >= neighbor.y - 1 && y <= neighbor.y + 1)) ||
                    (x == neighbor.x + 1 && y == neighbor.y))
                {
                    neighborCells.Add(neighbor);
                }
            }
            else
            {
                if (((x == neighbor.x || x == neighbor.x + 1) && (y >= neighbor.y - 1 && y <= neighbor.y + 1)) ||
                    (x == neighbor.x - 1 && y == neighbor.y))
                {
                    neighborCells.Add(neighbor);
                }
            }
        }
        neighborCells.Remove(this);
    }

    private List<Cell> CheckElementsNeighbors()
    {
        var result = new List<Cell>();
        foreach (Cell c in neighborCells)
        {
            if (c.GetCurrentElement() != null && currentElement.IsComparable(c.GetCurrentElement()))
            {
                result.Add(c);
            }
        }
        return result;
    }

    private void ChangeActive(bool value)
    {
        if (startedCell && !value) return;

        if (!value)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = unactivCell;
            img.color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = activCell;
            img.color = new Color(1f, 1f, 1f, 0.85f);
        }
        isCellActive = value;
        if (finishedCell && isCellActive) Win();
    }

    private void Win()
    {
        gameManager.GetComponent<GameControl>().WinGame();

    }

    private void AllCheckCell()
    {
        var n = GameObject.FindGameObjectsWithTag("cell");
        foreach (GameObject c in n)
        {
            Cell neighbor = c.GetComponent<Cell>();
            if (neighbor.GetCurrentElement() != null && neighbor.GetIsActiveCell())
            {
                neighbor.ChangeActive(false);
            }
        }
        GlobalValue.startCell.CheckStartLink();
    }

}
