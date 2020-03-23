using System.Security.Cryptography.X509Certificates;
using Minigames;
using UnityEngine;

namespace Interval
{
    public class ProgressItemFactory : MonoBehaviour
    {
        public RoundManager Rounds;
        [HideInInspector] public ProgressItem[] ProgressItems;

        private void Start()
        {
            Rounds.OnStartRound.AddListener(Create);
            Rounds.OnAllRoundsCompleted.AddListener(() => Create(Rounds.Rounds.Length));
        }

        public void Create(int i)
        {
            if (i <= 0) return;
            var pi = ProgressItems[i-1];
            if(pi)pi.Enable();
        }
    }
}