public class Bakery1Completed : Saveable
{
    public override bool Get()
    {
        if (GlobalVariables.BakeryState >= 2)
        {
            return true;
        }

        return false;
    }

    public override void Set(bool value)
    {
        if (value)
        {
            GlobalVariables.BakeryState = 2;
            return;
        }

        GlobalVariables.BakeryState--;
    }
}