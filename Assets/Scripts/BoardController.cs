using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public static BoardController instance;

    [SerializeField] bool buildOnStart;

    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameObject boardParent;

    [SerializeField] string placementString;

    private float tileSize;
    private Vector3 previousPiecePosition;

    private List<Tile> tileList;

    private void Awake()
    {
        instance = this;

        tileSize = tilePrefab.transform.localScale.x;
        tileList = new List<Tile>();
    }

    private void Start()
    {
        if (buildOnStart)
        {
            BuildBoard();
        }
    }

    public void BuildBoard(GameObject newBoardParent = null)
    {
        if (newBoardParent)
        {
            boardParent = newBoardParent;
        }

        for (int i = 0; i < placementString.Length; ++i)
        {
            PlaceTile(placementString[i]);
        }

        SetTileEdges();
    }

    void PlaceTile(char dir)
    {
        Vector3 spawnPosition;

        switch (dir)
        {
            case 's': spawnPosition = new Vector3(0, 0, 0); break;
            case 'u': spawnPosition = previousPiecePosition + new Vector3(0, 0, tileSize); break;
            case 'd': spawnPosition = previousPiecePosition + new Vector3(0, 0, -tileSize); break;
            case 'l': spawnPosition = previousPiecePosition + new Vector3(-tileSize, 0, 0); break;
            case 'r': spawnPosition = previousPiecePosition + new Vector3(tileSize, 0, 0); break;
            default: return;
        }
        
        previousPiecePosition = spawnPosition;

        GameObject newTile = Instantiate(tilePrefab, boardParent.transform);
        newTile.transform.localPosition = spawnPosition;

        tileList.Add(newTile.GetComponentInChildren<Tile>());
    }

    void SetTileEdges()
    {
        for (int i = 0; i < tileList.Count; ++i)
        {
            tileList[i].SetEdges(placementString[i], (i + 1 >= placementString.Length ? 'x' : placementString[(i + 1)]));
            tileList[i].gameObject.GetComponent<TileSpawnMovement>().StartMovement(i);
        }
    }
}
