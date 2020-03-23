using System.Collections.Generic;
using System.Linq;
using Sequencing;
using UnityEngine;
using UnityEngine.Events;

namespace Minigames
{
    [RequireComponent(typeof(Minigame))]
    public class RoundManager : MonoBehaviour
    {
        public UnityEvent OnAllRoundsCompleted;
        public DataUser Sampler;
        public Interactable[] Interactables;
        [HideInInspector] public Round[] Rounds;
        [HideInInspector] public UnityEvent<int> OnStartRound = new IntEvent();
        [HideInInspector] public DataProvisionStyle Style;
        [HideInInspector] public ScriptableObject Provider;

        private int _currentRound;

        void Awake()
        {
            switch (Style)
            {
                case DataProvisionStyle.Manual:
                    foreach (var ia in Interactables)
                    {
                        var du = ia.gameObject.GetComponent<DataUser>();
                        if(du == null) du = ia.gameObject.AddComponent<DataUser>();
                        du.Data = new Object[Rounds.Length];
                        for (var index = 0; index < Rounds.Length; index++)
                        {
                            var round = Rounds[index];
                            foreach (var answer in round.Answers)
                            {
                                if (answer.Interactable == ia)
                                {
                                    du.Data[index] = answer.Data;
                                }
                            }
                        }

                        OnStartRound.AddListener(du.SetRound);
                        ia.OnClick.AddListener(() => AnswerCurrentRound(ia));
                    }
                    OnStartRound.AddListener(Sampler.SetRound);
                    break;
            }           
        }

        void Start()
        {
            CheckIfPossible();
            
            if (Rounds == null || Rounds.Length == 0) return;
            StartRound(0);
        }

        private void CheckIfPossible()
        {
            for (int i = 0; i < Rounds.Length; i++)
            {
                if(Rounds[i].Answers.ToList().TrueForAll(a => a.IsCorrect == false))
                Debug.LogWarning("Round " + (i + 1) + " has no correct answers!");
            }
        }

        public void CompleteCurrentRound()
        {
            _currentRound++;
            if (Rounds.Length <= _currentRound)
            {
                OnAllRoundsCompleted.Invoke();
                return;
            }

           StartRound(_currentRound);
        }

        public void AnswerCurrentRound(Interactable i)
        {
            Rounds[_currentRound].Answer(GetMatchingAnswer(i));
        }

        private Answer GetMatchingAnswer(Interactable i)
        {
                foreach (var a in Rounds[_currentRound].Answers)
                {
                    if (a.Interactable == i) return a;
                }

            return new Answer();
        }

        private List<UnityAction<Answer>> _actions;
        private void StartRound(int i)
        {
            OnStartRound.Invoke(i);
            Rounds[i].Start(this);    
        }
    }
}