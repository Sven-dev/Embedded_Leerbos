using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeVoiceScript : MonoBehaviour {

    public List<AudioClip> HourClips;
    public List<AudioClip> ModifierClips;
    private AudioSource aSource;
    
	void Start () {
        aSource = GetComponent<AudioSource>();
	}


    public void PlayTimeSounds(int hour,int modifier)
    {
        switch(modifier)
        {
            case 0: case 1: case 2:
                StartCoroutine(_QueueSound(ModifierClips[modifier]));
                StartCoroutine(_QueueSound(HourClips[hour-1]));
                break;
            case 3:
                StartCoroutine(_QueueSound(HourClips[hour-1]));
                StartCoroutine(_QueueSound(ModifierClips[modifier]));
                break;
        }
    }

    IEnumerator _QueueSound(AudioClip clip)
    {
        while(aSource.isPlaying)
        {
            yield return null;
        }
        aSource.PlayOneShot(clip);
    }
}
