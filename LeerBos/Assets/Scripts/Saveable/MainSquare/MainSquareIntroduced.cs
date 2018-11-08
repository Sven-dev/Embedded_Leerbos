[System.Serializable]
public class MainSquareIntroduced : Saveable
{
    public override bool Get()
    {
        if (GlobalVariables.MainSquareState >= 1)
        {
            return true;
        }

        return false;
    }

    public override void Set(bool value)
    {
        if (value)
        {
            GlobalVariables.MainSquareState = 1;
            return;
        }

        GlobalVariables.MainSquareState--;
    }
}