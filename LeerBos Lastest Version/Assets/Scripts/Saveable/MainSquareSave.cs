public class MainSquareSave : Saveable
{
    public override int Value
    {
        get { return GlobalVariables.MainSquareState; }

        set { GlobalVariables.MainSquareState = value; }
    }
}