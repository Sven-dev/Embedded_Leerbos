public class TowerSave : Saveable
{
    public override int Value
    {
        get { return GlobalVariables.TowerState; }

        set { GlobalVariables.TowerState = value; }
    }
}
