using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressKartScript : MonoBehaviour {
    public List<Sprite> sprites;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void FillKart(List<bool> itemsPresent)
    {
        if (!itemsPresent[0])
        {
            if (!itemsPresent[1])
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
            if (!itemsPresent[1])
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
