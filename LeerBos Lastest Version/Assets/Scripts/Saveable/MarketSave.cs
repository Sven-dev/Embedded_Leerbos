public class MarketSave : Saveable
{
    public override int Value
    {
        get { return GlobalVariables.MarketState; }

        set { GlobalVariables.MarketState = value; }
    }
}