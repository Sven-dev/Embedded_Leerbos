namespace Balloons.Scripts
{
    public struct Answer
    {
        public bool IsCorrect;
        public string Text;

        public Answer(string text, bool correct)
        {
            IsCorrect = correct;
            Text = text;
        }
    }
}