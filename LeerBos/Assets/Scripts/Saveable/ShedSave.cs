public class ShedSave : Saveable
{
    public override int Value
    {
        get { return GlobalVariables.ShedState; }
        set { GlobalVariables.ShedState = value; }
    }
}