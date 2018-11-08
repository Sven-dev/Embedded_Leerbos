public class Market2Introduced : Saveable
{
    public override bool Get()
    {
        if (GlobalVariables.MarketState >= 3)
        {
            return true;
        }

        return false;
    }

    public override void Set(bool value)
    {
        if (value)
        {
            GlobalVariables.MarketState = 3;
            return;
        }

        GlobalVariables.MarketState--;
    }

}