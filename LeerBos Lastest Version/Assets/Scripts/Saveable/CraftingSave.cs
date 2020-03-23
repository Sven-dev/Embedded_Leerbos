public class CraftingSave : Saveable
{
    public override int Value
    {
        get { return GlobalVariables.CraftingState; }

        set { GlobalVariables.CraftingState = value; }
    }
}
