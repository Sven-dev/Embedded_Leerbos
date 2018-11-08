using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : SceneSwitchable
{
    public Npc Character;

    protected override void Click(Vector3 clickposition)
    {
        if (TargetScene != null)
        {
            base.Click(clickposition);
        }
        else
        {
            Character.PlayDialogue(Character.Wrongwayclips);
        }
    }
}
