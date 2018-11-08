public class Market1Completed : Saveable
{
    public override bool Get()
    {
        if (GlobalVariables.MarketState >= 2)
        {
            return true;
        }

        return false;
    }

    public override void Set(bool value)
    {
        if (value)
        {
            GlobalVariables.MarketState = 2;
            return;
        }

        GlobalVariables.MarketState--;
    }
}