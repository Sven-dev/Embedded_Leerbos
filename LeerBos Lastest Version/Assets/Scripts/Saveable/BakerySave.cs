public class BakerySave : Saveable
{
    public override int Value
    {
        get { return GlobalVariables.BakeryState; }

        set { GlobalVariables.BakeryState = value; }
    }
}