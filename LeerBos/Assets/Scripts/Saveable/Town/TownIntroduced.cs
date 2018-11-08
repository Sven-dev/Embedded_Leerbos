using System;

[Serializable]
public class TownIntroduced : Saveable
{
    public override bool Get()
    {
        if (GlobalVariables.TownState >= 1)
        {
            return true;
        }

        return false;
    }

    public override void Set(bool value)
    {
        if (value)
        {
            GlobalVariables.TownState = 1;
            return;
        }

        GlobalVariables.TownState--;
    }
}