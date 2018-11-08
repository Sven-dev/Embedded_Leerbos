public class Bakery1Introduced : Saveable
{
    public override bool Get()
    {
        if (GlobalVariables.BakeryState >= 1)
        {
            return true;
        }

        return false;
    }

    public override void Set(bool value)
    {
        if (value)
        {
            GlobalVariables.BakeryState = 1;
            return;
        }

        GlobalVariables.BakeryState--;
    }
}