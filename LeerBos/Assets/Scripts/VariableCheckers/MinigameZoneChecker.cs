using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameZoneChecker : ZoneChecker
{
    [Space]
    public List<Saveable> Minigames;
    [Space]
    public Saveable ZoneComplete;

    protected override void Check()
    {
        bool Completed = true;
        foreach (Saveable minigame in Minigames)
        {

            if (minigame.Get() == false)
            {
                Completed = false;
            }
        }

        if (Completed)
        {
            ZoneComplete.Set(true);
        }

        base.Check();
    }
}