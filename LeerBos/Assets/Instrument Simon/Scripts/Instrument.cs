using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : Interactable
{
    public AudioClip Consonant;
    public AudioClip Dissonant;

    [HideInInspector]
    public AudioSource Audio;
    private InstrumentPlayer Manager;
    private SpriteRenderer Outline;

	// Use this for initialization
	private void Awake ()
    {
        Audio = GetComponent<AudioSource>();
        Manager = transform.parent.GetComponent<InstrumentPlayer>();
        Outline = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}
    protected override void Click(Vector3 clickposition)
    {
        //If you were allowed to hit the instrument
        if (Manager.Throwable)
        {
            //Check if the instrument is the right one
            Manager.CheckInstrument(this);
        }    
    }

    //Play a consonant sound
    public void PlayConsonant()
    {
        Audio.clip = Consonant;
        StartCoroutine(_Play(Consonant));
    }

    //Play a consonant sound with a custom pitch
    public void PlayConsonant(float pitch)
    {
        Audio.clip = Consonant;
        StartCoroutine(_Play(Consonant, pitch));
    }

    //Play a dissonant sound
    public void PlayDissonant()
    {
        StartCoroutine(_Play(Dissonant));
    }

    private IEnumerator _Play(AudioClip clip, float pitch = 1)
    {
        Outline.enabled = true;

        Audio.clip = clip;
        Audio.pitch = pitch;
        Audio.Play();
        while(Audio.isPlaying)
        {
            yield return null;
        }

        Audio.pitch = 1;
        yield return new WaitForSeconds(0.25f);
        Outline.enabled = false;
    }
}