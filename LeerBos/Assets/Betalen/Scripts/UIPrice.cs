using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPrice : MonoBehaviour
{
    private Text PriceLabel;
    private Register Register;

    private void Awake()
    {
        PriceLabel = GetComponent<Text>();
    }

    public void Link(Register r)
    {
        Register = r;
        r.OnPriceChange += UpdateUI;
    }

    private void UpdateUI()
    {
        PriceLabel.text = "€" + Register.Price.ToString("0.00");
    }
}
