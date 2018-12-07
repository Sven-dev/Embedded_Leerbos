using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAreaChecker : AreaChecker
{
    [Space]
    public List<Saveable> GameStates;
    public GameObject[] Indicators;

    protected override void Check()
    {

        foreach (Saveable gamestate in GameStates)
        {
            //if the game has been completed for the first time
            if (gamestate.Value == 2)
            {
                print("test");
                gamestate.Value = 3;

                //Checks if all minigames in the area have been completed
                bool AreaCompleted = true;
                foreach(Saveable Game in GameStates)
                {
                    if (Game.Value < 2)
                    {
                        AreaCompleted = false;
                    }
                }

                if (AreaCompleted)
                {
                    AreaState.Value = 2;
                }
            }
            //if the game has been completed earlier
            else if (gamestate.Value == 3)
            {
                //fill kart /w item
            }
        }

        //if one or multiple games have been completed
        for (int i = 0; i < GameStates.Count; i++)
        {
            if (GameStates[i].Value == 3)
            {
                Destroy(Indicators[i]);
            }
        }

        base.Check();
    }
}