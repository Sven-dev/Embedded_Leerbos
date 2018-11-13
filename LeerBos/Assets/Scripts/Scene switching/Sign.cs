using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : SceneSwitchable
{
    public Npc Character;

    protected override void Click(Vector3 clickposition)
    {
        if (TargetString != "")
        {
            base.Click(clickposition);
        }
        else
        {
            Character.PlayDialogue(Character.Wrongwayclips);
        }
    }

    public void Blink()
    {
        StartCoroutine(_Blink());
    }

    IEnumerator _Blink()
    {
        yield return null;
    }
}