using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace Sequencing{

    public class Sequencable : MonoBehaviour
    {
        [HideInInspector]public float IntervalAfter;
        public UnityEvent OnTurn;
        public UnityEvent OnEndTurn;

        public AudioSource Audio;
        
        public IEnumerator GiveTurn()
        {
            OnTurn.Invoke();
            Audio.PlayOneShot(GetComponent<DataUser>().ReturnCurrentData<AudioClip>());
            yield return new WaitWhile(() => Audio.isPlaying);
            OnEndTurn.Invoke();
        }

        public void Interrupt()
        {      
            Audio.Stop();
            StopAllCoroutines();
            OnEndTurn.Invoke();
        }
    }
}