using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChange : MonoBehaviour {

    private GridManager Grid;
    private double Change;
    private Text ChangeLabel;

    private void Awake()
    {
        ChangeLabel = GetComponent<Text>();
    }

    public void Link(GridManager grid)
    {
        grid.OnChangeChange += UpdateUI;
        Grid = grid;
        Change = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        StartCoroutine(_UpdateUI());
    }

    IEnumerator _UpdateUI()
    {
        int signum = Grid.Change.CompareTo(Change);
        while (Change != Grid.Change)
        {
            Change += 0.01f * signum;
            ChangeLabel.text = "€" + Change;
            yield return null;
        }
    }
}