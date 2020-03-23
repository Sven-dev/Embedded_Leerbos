using System.Collections;
using System.Collections.Generic;
using Minigames;
using UnityEngine;
using UnityEngine.UI;

namespace Balloons.Scripts
{
    [RequireComponent(typeof(Minigame))]
    public class BalloonGameManager : MonoBehaviour
    {
        public BalloonController Controller;
        private AnswerManager _answerManager;
        public Text RabbitText;

        private List<Answer> _correctAnswers;

        private void Start()
        {
            _answerManager = GetComponent<AnswerManager>();
            StartRound();
        }

        private int _currentRound;

        public void StartRound()
        {
            _currentRound++;
            if (_currentRound > 3)
            {
                GetComponent<Minigame>().Complete();
                return;
            }
            StartCoroutine(SpawnBalloons());
        }

        IEnumerator SpawnBalloons()
        {
            yield return Controller.PopBalloons();
            _correctAnswers = new List<Answer>();
            int count = 6;
            var balloons = Controller.SpawnBalloons(count);
            var answers = _answerManager.Get();
            RabbitText.text = answers[0].Text;
            for(int i = 0; i < count; i++)
            {
                Answer a = answers[i+1];
                var b = balloons[i];
                b.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2 * i;
                b.GetComponentInChildren<Canvas>().sortingOrder = 2 * i + 1;
                var t = b.GetComponentInChildren<BalloonUi>();
                t.Text.text = a.Text;
                t.IsCorrect = a.IsCorrect;
                if (a.IsCorrect)
                {
                    _correctAnswers.Add(a);
                    b.OnHit.AddListener(() =>
                    {
                        _correctAnswers.Remove(a);
                        CheckIfDone();
                    });
                }
            }
        }

        void CheckIfDone()
        {
            if (_correctAnswers.Count == 0)
            {
                StartRound();
            }
        }

    }
}