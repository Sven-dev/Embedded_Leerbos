using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : MonoBehaviour, I_SmartwallInteractable
{
    public List<AudioClip> Consonants;
    [Space]
    public AudioClip Dissonant;

    [HideInInspector]
    public AudioSource Audio;
    private InstrumentPlayer Manager;
    private SpriteRenderer Outline;

	// Use this for initialization
	private void Awake ()
    {
        Audio = GetComponent<AudioSource>();
        Manager = transform.parent.parent.GetComponent<InstrumentPlayer>();
        Outline = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}

    public void Hit(Vector3 clickposition)
    {
        //If you were allowed to hit the instrument
        if (Manager.Throwable)
        {
            StartCoroutine(_Click());
            if (Manager.Free)
            {
                PlayConsonant(7);
            }
            else
            {
                //Check if the instrument is the right one
                Manager.CheckInstrument(this);
            }
        }
    }

    //Makes the object shrink a bit when hit, makes it look like it gets pressed
    IEnumerator _Click()
    {
        Vector3 scale = transform.localScale;

        for (int i = 0; i < 2; i++)
        {
            transform.localScale -= scale * 0.1f;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < 2; i++)
        {
            transform.localScale += scale * 0.1f;
            yield return null;
        }
    }

    //Play a consonant sound with a custom pitch
    public void PlayConsonant(int pitch)
    {
        StartCoroutine(_Play(Consonants[pitch]));
    }

    //Play a dissonant sound
    public void PlayDissonant()
    {
        StartCoroutine(_Play(Dissonant));
    }

    //Play an audio-clip, sometimes with a custom pitch
    private IEnumerator _Play(AudioClip clip)
    {
        Outline.enabled = true;

        Audio.clip = clip;
        Audio.Play();
        while(Audio.isPlaying)
        {
            yield return null;
        }

        Audio.pitch = 1;
        Outline.enabled = false;
    }
}