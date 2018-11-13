public class Market2Completed : Saveable
{
    public override bool Get()
    {
        if (GlobalVariables.MarketState >= 4)
        {
            return true;
        }

        return false;
    }

    public override void Set(bool value)
    {
        if (value)
        {
            GlobalVariables.MarketState = 4;
            return;
        }

        GlobalVariables.MarketState--; 
    }
}