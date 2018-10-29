using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Spawn percentages for each prefab.")]
    [Range(0, 1)]
    public float StraightChance;
    [Range(0, 1)]
    public float CornerChance;
    [Range(0, 1)]
    public float PitChance;

    [Space]
    public List<Block> Prefabs;
    private Block[,] Grid;
    public List<CoinSpawner> Spawners;

    // Use this for initialization
    void Start ()
    {
        SpawnGrid();
        LinkGrid();
        ReconnectGrid();
	}

    void LinkGrid()
    {
        foreach (Block b in Grid)
        {
            if (b is ConveyorBlock)
            {
                ConveyorBlock cb = b as ConveyorBlock;
                cb.Connector.OnConnectionsChanged += ReconnectGrid;
            }
        }
    }

    void ReconnectGrid()
    {
        foreach (Block b in Grid)
        {
            if (b is ConveyorBlock)
            {
                ConveyorBlock cb = b as ConveyorBlock;
                cb.Connector.Active = false;
            }
        }

        foreach (CoinSpawner s in Spawners)
        {
            s.Connector.Power();
        }

        foreach (Block b in Grid)
        {
            if (b is ConveyorBlock)
            {
                ConveyorBlock cb = b as ConveyorBlock;
                cb.Connector.PowerUsed = false;
            }
        }

        foreach (CoinSpawner s in Spawners)
        {
            s.Connector.PowerUsed = false;
        }
    }

    void SpawnGrid()
    {
        int rows = transform.childCount;
        int items = transform.GetChild(0).childCount;

        Grid = new Block[rows, items];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < items; j++)
            {
                SpawnBlock(i, j);
            }
        }
    }

    void SpawnBlock(int x, int y)
    {
        float rnd = Random.Range(0, StraightChance + CornerChance + PitChance);
        Block b;

        if (rnd < StraightChance)
        {
            b = Prefabs[0];
        }
        else if (rnd < CornerChance + StraightChance)
        {
            b = Prefabs[1];
        }
        else //if (rnd < PitChance + CornerChance + StraightChance)
        {
            b = Prefabs[2];
        }

        Grid[x,y] = Instantiate(b, transform.GetChild(x).GetChild(y));
    }


}