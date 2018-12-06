using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownChecker : ZoneChecker
{
    public List<Saveable> GameStates;
    public GameObject[] Indicators;

    protected override void Check()
    {
        for (int i = 0; i < GameStates.Count; i++)
        {
            if(GameStates[i].Get() == true)
            {
                Destroy(Indicators[i]);
            }
        }

        base.Check();
    }
}
