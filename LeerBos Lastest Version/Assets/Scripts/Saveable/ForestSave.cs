public class ForestSave : Saveable
{
    public override int Value
    {
        get { return GlobalVariables.ForestState; }

        set { GlobalVariables.ForestState = value; }
    }
}
