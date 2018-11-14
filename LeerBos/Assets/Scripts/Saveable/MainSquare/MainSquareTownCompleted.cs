[System.Serializable]
//Checks if the town area was the LATEST area cleared
public class MainSquareTownCompleted : Saveable
{
    public override bool Get()
    {
        if (GlobalVariables.MainSquareState == 3)
        {
            return true;
        }

        return false;
    }

    public override void Set(bool value)
    {
        if (value)
        {
            GlobalVariables.MainSquareState = 3;
            return;
        }

        GlobalVariables.MainSquareState--;
    }
}