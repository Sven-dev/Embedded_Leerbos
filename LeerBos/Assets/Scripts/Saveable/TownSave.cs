public class TownSave : Saveable
{
    public override int Value
    {
        get { return GlobalVariables.TownState; }

        set { GlobalVariables.TownState = value; }
    }
}