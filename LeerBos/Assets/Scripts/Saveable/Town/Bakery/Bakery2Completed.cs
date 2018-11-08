public class Bakery2Completed : Saveable
{
    public override bool Get()
    {
        if (GlobalVariables.BakeryState >= 4)
        {
            return true;
        }

        return false;
    }

    public override void Set(bool value)
    {
        if (value)
        {
            GlobalVariables.BakeryState = 4;
            return;
        }

        GlobalVariables.BakeryState--;
    }
}