using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoradCells : MonoBehaviour
{
    [SerializeField] private Transform hexPrefab;
    [SerializeField] private int gridWidth = 8;
    [SerializeField] private int gridHeight = 5;
    [SerializeField] private float gap = 0;
    private float hexWidth, hexHeight;
    private Dictionary<Vector2, Cell> allCells;

    private Vector3 startPos;

    void Start()
    {
        hexWidth = 1;
        hexHeight = 1;
        allCells = new Dictionary<Vector2, Cell>();
        CalcSizeHex();
        AddGap();
        CalcStartPos();
        CreateGrid();
        GenerateMapsMirror();
        GetNeighborsAllCels();
        SetStartAndFinishCell();
    }

    private void GetNeighborsAllCels()
    {
        foreach (Cell cell in allCells.Values)
        {
            cell.FindAllNeighbors(allCells);
        }
    }

    private void CalcSizeHex()
    {
        float scaler = gameObject.GetComponent<RectTransform>().sizeDelta.y * 0.9f / gridHeight;
        hexHeight *= scaler;
        hexWidth *= scaler;
    }

    private void AddGap()
    {
        hexWidth += gap / 2;
        hexHeight += gap / 2;
    }

    private void CalcStartPos()
    {
        float offset = 0;
        if (gridHeight / 2 % 2 != 0)
            offset = hexWidth / 2;

        float x = -gameObject.GetComponent<RectTransform>().sizeDelta.x - gameObject.GetComponent<RectTransform>().sizeDelta.x / 2;
        float y = gameObject.GetComponent<RectTransform>().sizeDelta.y / 2 - gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;

        var sizeStarter = gameObject.GetComponent<RectTransform>().sizeDelta;
        var posStarter = gameObject.transform.position;

        startPos = new Vector2((posStarter.x - sizeStarter.x / 2) + hexWidth / 2 + (sizeStarter.x - hexWidth * gridWidth) / 2,
                               (posStarter.y + sizeStarter.y / 2) - hexHeight / 2 - (sizeStarter.y - hexHeight * gridHeight) / 2);
    }

    private Vector3 CalcWorldPos(Vector2 gridPos)
    {
        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = hexWidth / 2;

        float x = (startPos.x + gridPos.x * hexWidth + offset);
        float y = startPos.y - gridPos.y * hexHeight * 0.95f;

        return new Vector3(x, y, 0);
    }

    private void CreateGrid()
    {
        var startCellCord = new Vector2(Random.Range(0, 1), Random.Range(0, gridHeight - 1));
        var finishCellCord = new Vector2(Random.Range(gridWidth - 1, gridWidth), Random.Range(0, gridHeight - 1));

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                Transform hex = Instantiate(hexPrefab) as Transform;
                Vector2 gridPos = new Vector2(x, y);
                hex.localScale = new Vector3(hexWidth - gap, hexHeight - gap, 1);
                hex.position = CalcWorldPos(gridPos);
                hex.SetParent(this.transform, false);
                hex.name = "Hexagon" + x + "|" + y;
                var cell = hex.GetComponent<Cell>();
                cell.x = x;
                cell.y = y;
                cell.OnCreate();

                allCells.Add(new Vector2(x, y), cell);
            }
        }
    }

    private void FindWay()
    {
        var startCell = GlobalValue.startCell;
        var finishCell = GlobalValue.finishCell;
    }

    private void GenerateMapsMirror()
    {
        int rIter = Random.Range(gridHeight, (gridWidth * gridHeight) / 4);
        for (int i = 0; i < rIter; i++)
        {

            Vector2 rPos = new Vector2(Random.Range(0, gridWidth / 2), Random.Range(0, gridHeight / 2));
            Vector2 twoPos = new Vector2((gridWidth - rPos.x) - 1, (gridHeight - rPos.y) - 1 );
            
            if (allCells.ContainsKey(rPos) && allCells.ContainsKey(twoPos))
            {
                Destroy(allCells[rPos].gameObject);
                allCells.Remove(rPos);
                if (twoPos.x != rPos.x && twoPos.y != rPos.y)
                {
                    Destroy(allCells[twoPos].gameObject);
                    allCells.Remove(twoPos);
                }
                
            }
        }
    }

    private void SetStartAndFinishCell()
    {
        for (var i = 0; i < 10; i++)
        {
            var startCellCord = new Vector2(Random.Range(0, 1), Random.Range(0, gridHeight - 1));
            var finishCellCord = new Vector2(Random.Range(gridWidth - 1, gridWidth), Random.Range(0, gridHeight - 1));

            if (allCells.ContainsKey(startCellCord) && allCells.ContainsKey(finishCellCord))
            {
                if (allCells[startCellCord].GetNeigbors().Count > 0 && allCells[finishCellCord].GetNeigbors().Count > 0) 
                {
                    allCells[startCellCord].OnCreate(isStart: true);
                    allCells[finishCellCord].OnCreate(isFinish: true);
                    break;
                }

            }
        }
        if (!GlobalValue.startCell || !GlobalValue.finishCell)
        {
            Start();
        }

    }

}
