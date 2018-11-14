using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayButton : SceneSwitchable
{
    protected override void Click(Vector3 clickposition)
    {
        GlobalVariables.Reset();
        base.Click(clickposition);
    }
}