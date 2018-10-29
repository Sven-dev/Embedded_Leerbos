using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : Interactable
{
    protected Image Image;

    private void Awake()
    {
        Image = GetComponent<Image>();
    }
}