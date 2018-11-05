using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPrice : MonoBehaviour
{
    private Text PriceLabel;
    private Counter Counter;

    private void Awake()
    {
        PriceLabel = GetComponent<Text>();
    }

    public void Link(Counter c)
    {
        Counter = c;
        c.OnPriceChange += UpdateUI;
    }

    private void UpdateUI()
    {
        PriceLabel.text = "€" + Counter.Price.ToString("0.00");
    }
}
