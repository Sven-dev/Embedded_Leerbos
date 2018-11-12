public class MainSquareCompleted : Saveable
{
    public override bool Get()
    {
        if (GlobalVariables.MainSquareState >= 2)
        {
            return true;
        }

        return false;
    }

    public override void Set(bool value)
    {
        if (value)
        {
            GlobalVariables.MainSquareState = 2;
            return;
        }

        GlobalVariables.BakeryState--;
    }
}