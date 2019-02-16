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
        Manager.CheckInstrument(this);
    }

    public void PlayConsonant()
    {
        Audio.clip = Consonant;
        StartCoroutine(_Play());
    }


    public void PlayDissonant()
    {
        Audio.clip = Dissonant;
        StartCoroutine(_Play());
    }
    private IEnumerator _Play()
    {
        Outline.enabled = true;
        Audio.Play();
        while(Audio.isPlaying)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.25f);
        Outline.enabled = false;
    }
}