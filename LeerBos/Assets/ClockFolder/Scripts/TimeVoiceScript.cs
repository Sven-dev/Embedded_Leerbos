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
            //if modifiers 0, 1 and 2, say "modifier, hour"
            case 0: case 1: case 2:
                StartCoroutine(_QueueSound(ModifierClips[modifier]));
                StartCoroutine(_QueueSound(HourClips[hour-1]));
                break;
            //if modifier 3, say "hour, modifier"
            case 3:
                StartCoroutine(_QueueSound(HourClips[hour-1]));
                StartCoroutine(_QueueSound(ModifierClips[modifier]));
                break;
        }
    }

    IEnumerator _QueueSound(AudioClip clip)
    {
        //wait for current sound to stop playing
        while(aSource.isPlaying)
        {
            yield return null;
        }
        //play sound
        aSource.PlayOneShot(clip);
    }
}
