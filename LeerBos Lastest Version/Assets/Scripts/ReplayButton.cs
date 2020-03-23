using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayButton : SceneSwitchable, I_SmartwallInteractable
{
    public void Hit(Vector3 clickposition)
    {
        GlobalVariables.Reset();
        base.Hit(clickposition);
    }
}