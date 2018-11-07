public class BakerySwitcher : MultiSceneSwitcher
{
    protected override void Compare()
    {
        if (!GlobalVariables.Bakery1Introduced)
            SceneSwitcher.Switch(TargetScenes[0], TransitionImages[0]);
        else if (!GlobalVariables.Bakery1Completed)
            SceneSwitcher.Switch(TargetScenes[1], TransitionImages[1]);
        else if (!GlobalVariables.Bakery2Introduced)
            SceneSwitcher.Switch(TargetScenes[2], TransitionImages[2]);
        else if (!GlobalVariables.Bakery2Completed)
            SceneSwitcher.Switch(TargetScenes[3], TransitionImages[3]);
        else
            throw new System.Exception("UnknownSceneException: save file does not use the right save-file variables");
    }
}