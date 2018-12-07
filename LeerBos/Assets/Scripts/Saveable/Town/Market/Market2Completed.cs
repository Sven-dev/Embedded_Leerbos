public class Market2Completed : Saveable
{
    public override bool Get()
    {
        if (GlobalVariables.MarketState >= 5)
        {        
            return true;
        }

        return false;
    }

    public override void Set(bool value)
    {
        if (value)
        {
            GlobalVariables.MarketState = 5;
            return;
        }

        GlobalVariables.MarketState--; 
    }
}