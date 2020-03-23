using System.Linq;
using UnityEngine;

namespace Balloons.Scripts
{
    public class AnswerManager : MonoBehaviour
    {
        private int _difficulty;
        void Awake()
        {
            _difficulty = PlayerPrefs.HasKey("difficulty") ? PlayerPrefs.GetInt("difficulty") : 0;
            _answers = _difficulty == 0 ? EasyAnswers : _difficulty == 1 ? MediumAnswers : HardAnswers;
            _answers = _answers.Shuffle().ToArray();
        }

        private int _current;
        private Answer[][] _answers;

        public Answer[] Get()
        {
            var ret = _answers[_current%3];
            _current += 1;
            return ret;
        }
        public Answer[][] EasyAnswers =
        {
            new []
            {
                new Answer("7 + 2", true),
                
                new Answer("5 + 4", true),
                new Answer("3 + 7", false),
                new Answer("2 + 7", true),
                new Answer("5 + 4", true),
                new Answer("9 + 0", true),
                new Answer("8 + 5", false)                
            },
            new []
            {
                new Answer("4 + 3", true),
                
                new Answer("3 + 4", true),
                new Answer("5 + 4", false),
                new Answer("2 + 5", true),
                new Answer("5 + 2", true),
                new Answer("7 + 1", false),
                new Answer("2 + 1", false)                
            },
            new []
            {
                new Answer("8 + 1", true),
                
                new Answer("5 + 4", true),
                new Answer("7 + 4", false),
                new Answer("5 + 4", true),
                new Answer("3 + 6", true),
                new Answer("2 + 7", true),
                new Answer("8 + 5", false)                
            }          
        };

        public Answer[][] MediumAnswers;
        public Answer[][] HardAnswers;

    }
}