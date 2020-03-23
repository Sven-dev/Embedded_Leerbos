using UnityEngine;

namespace Interval
{
    [RequireComponent(typeof(AudioSource))]
    public class ProgressItem : MonoBehaviour
    {
        public AudioClip AudioOnAcquire;
        
        void Start()
        {
            gameObject.SetActive(false);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
            if(AudioOnAcquire) GetComponent<AudioSource>().PlayOneShot(AudioOnAcquire);
        }
    }
}