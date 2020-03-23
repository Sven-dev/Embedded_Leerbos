public class MountainSave : Saveable
{
    public override int Value
    {
        get { return GlobalVariables.MountainState; }

        set { GlobalVariables.MountainState = value; }
    }
}