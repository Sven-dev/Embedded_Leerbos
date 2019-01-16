using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAreaChecker : AreaChecker
{
    [Space]
    public List<ProgressItemOverworld> ProgressItems;
    public ProgressKartScript ProgressKart;
    public List<Saveable> GameStates;
    public GameObject[] Indicators;

    [Space]
    public Sign MainSquareSign;
    private List<bool> itemsPresent = new List<bool> { false, false };

    protected override void Check()
    {
        for (int i = 0; i < GameStates.Count; i++)
        {
            //if the game has been completed earlier
            if (GameStates[i].Value == 3)
            {
                itemsPresent[i] = true;
            }

            //if the game has been completed for the first time
            if (GameStates[i].Value == 2)
            {
                ProgressItems[i].PlayAnimation();
                ProgressKart.FillKart();
                GameStates[i].Value = 3;
            }
        }

        //Checks if all minigames in the area have been completed
        bool AreaCompleted = true;
        foreach (Saveable Game in GameStates)
        {
            if (Game.Value < 2)
            {
                AreaCompleted = false;
            }
        }

        if (AreaCompleted)
        {
            AreaState.Value = 2;
            MainSquareSign.TargetString = "Outro";
        }

        //checks if the gameset has been completed, and destroys the associated indicator
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