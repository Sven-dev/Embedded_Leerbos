public class MazeSave : Saveable
{
    public override int Value
    {
        get { return GlobalVariables.MazeState; }

        set { GlobalVariables.MazeState = value; }
    }
}
