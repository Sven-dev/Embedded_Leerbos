using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingKartMover : Interactable {

    public Kart Kart;

    protected override void Click(Vector3 clickposition)
    {
        Kart.Move(clickposition);
    }
}
