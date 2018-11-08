public class TownCompleted : Saveable
{
    public override bool Get()
    {
        if (GlobalVariables.TownState >= 2)
        {
            return true;
        }

        return false;
    }

    public override void Set(bool value)
    {
        if (value)
        {
            GlobalVariables.TownState = 2;
            return;
        }

        GlobalVariables.TownState--;
    }
}