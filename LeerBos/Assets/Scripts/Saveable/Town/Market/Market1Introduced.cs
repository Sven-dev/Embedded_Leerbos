public class Market1Introduced : Saveable
{
    public override bool Get()
    {
        if (GlobalVariables.MarketState >= 1)
        {
            return true;
        }

        return false;
    }

    public override void Set(bool value)
    {
        if (value)
        {
            GlobalVariables.MarketState = 1;
            return;
        }

        GlobalVariables.MarketState--;
    }
}