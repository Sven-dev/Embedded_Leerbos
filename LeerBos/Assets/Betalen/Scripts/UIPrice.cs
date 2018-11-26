using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPrice : MonoBehaviour
{
    private Text PriceLabel;

    private void Start()
    {
        PriceLabel = GetComponent<Text>();
        Register.OnPriceChange += UpdateUI;
    }

    private void UpdateUI(double price)
    {
        PriceLabel.text = "€" + price.ToString("0.00");
        PriceLabel.text = PriceLabel.text.Replace(".", ",");
    }

    private void OnDestroy()
    {
        Register.OnPriceChange -= UpdateUI;
    }
}