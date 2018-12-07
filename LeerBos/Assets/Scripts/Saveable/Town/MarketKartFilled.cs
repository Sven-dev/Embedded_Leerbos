using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketKartFilled : Saveable
{
    public override bool Get()
    {
        if (GlobalVariables.MarketState == 4)
        {
            return true;
        }

        return false;
    }

    public override void Set(bool value)
    {
        if (value)
        {
            GlobalVariables.BakeryState = 4;
            return;
        }

        GlobalVariables.BakeryState--;
    }
}
