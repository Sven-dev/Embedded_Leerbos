public class FlowerSave : Saveable
{
    public override int Value
    {
        get { return GlobalVariables.FlowerState; }

        set { GlobalVariables.FlowerState = value; }
    }
}