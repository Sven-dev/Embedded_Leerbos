public class MusicSave : Saveable
{
    public override int Value
    {
        get { return GlobalVariables.MusicState; }

        set { GlobalVariables.MusicState = value; }
    }
}