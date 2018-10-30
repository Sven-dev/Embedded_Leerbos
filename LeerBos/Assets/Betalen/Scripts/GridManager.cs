using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Spawn percentages for each prefab.")]
    [Range(0, 1)]
    public float StraightChance;

    [Range(0, 1)]
    public float PitChance;

    [Space]
    public List<Block> Prefabs;
    private Block[,] Grid;
    public List<CoinSpawner> Spawners;

    public delegate void ChangeChange();
    public event ChangeChange OnChangeChange;
    public UIChange ChangeLabel;

    public double Change;
    // Use this for initialization
    void Start ()
    {
        SpawnGrid();
        ChangeLabel.Link(this);
	}

    void SpawnGrid()
    {
        int rows = transform.childCount;
        int items = transform.GetChild(0).childCount;

        Grid = new Block[rows, items];
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < items; y++)
            {
                SpawnBlock(x, y);
            }
        }
    }

    void SpawnBlock(int x, int y)
    {
        Transform griditem = transform.GetChild(x).GetChild(y);
        if (griditem.childCount == 0)
        {
            float rnd = Random.Range(0, StraightChance + PitChance);
            Block b;

            if (rnd < StraightChance)
            {
                b = Prefabs[0];
            }
            else //if (rnd < CornerChance + StraightChance)
            {
                b = Prefabs[1];
            }

            Grid[x, y] = Instantiate(b, griditem);
        }
    }

    public void AddChange(float amount)
    {

    }
}