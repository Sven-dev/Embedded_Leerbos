using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressKartScript : MonoBehaviour
{
    public List<Sprite> sprites;
    public List<bool> ItemsPresent;
    public Image image;

    public void FillKart()
    {
        if (GlobalVariables.MarketState < 3)
        {
            if (GlobalVariables.BakeryState < 3)
            {
                image.sprite = sprites[0];
            }
            else
            {
                image.sprite = sprites[2];
            }
        }
        else
        {
            if (GlobalVariables.BakeryState < 3)
            {
                image.sprite = sprites[1];
            }
            else
            {
                image.sprite = sprites[3];
            }
        }
    }
}
