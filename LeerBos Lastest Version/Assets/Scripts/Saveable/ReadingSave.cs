public class ReadingSave : Saveable
{
    public override int Value
    {
        get { return GlobalVariables.ReadingState; }

        set { GlobalVariables.ReadingState = value; }
    }
}