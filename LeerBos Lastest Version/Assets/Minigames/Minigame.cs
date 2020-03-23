using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Minigames
{
    public class Minigame : MonoBehaviour
    {
        public UnityEvent OnComplete;
        public float OnCompleteDelay;

        public void Complete()
        {
            StartCoroutine(End());
        }

        private IEnumerator End()
        {
            yield return new WaitForSeconds(OnCompleteDelay);
            OnComplete.Invoke();
        }
    }
}