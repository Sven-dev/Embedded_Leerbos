using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : Interactable
{
    private AudioSource Audio;
    public bool DialoguePlaying;

    [Space]
    public AudioClip HitClip;
    public List<AudioClip> IntroClips;
    public List<AudioClip> Wrongwayclips;
    public List<AudioClip> VictoryClips;
    public List<AudioClip> AfkClips;

    [Space]
    public List<OutlineBlinker> Blinkables;

    public float AfkTimer;
    private float AfkTime;

    // Use this for initialization
    void Start ()
    {
        DialoguePlaying = false;
        Audio = GetComponent<AudioSource>();
        AfkTime = 0;
        StartCoroutine(_AfkTimer());
	}

    public void PlayDialogue(List<AudioClip> clips)
    {
        if (!DialoguePlaying)
        {
            StartCoroutine(_PlayDialogue(clips));
        }
    }

    public void PlayDialogue(AudioClip clip)
    {
        if (!DialoguePlaying)
        {
            StartCoroutine(_PlayDialogue(clip));
        }
    }

    public void ActivateBlinkables()
    {
        foreach (OutlineBlinker blinker in Blinkables)
        {
            blinker.Blink();
        }
    }

    protected override void Click(Vector3 clickposition)
    {
        if (!DialoguePlaying)
        {
            PlayDialogue(HitClip);
        }
    }

    IEnumerator _PlayDialogue(List<AudioClip> clips)
    {
        AfkTime = 0;

        int index = 0;
        DialoguePlaying = true;
        while (index < clips.Count)
        {
            Audio.PlayOneShot(clips[index]);
            while (Audio.isPlaying)
            {
                yield return null;
            }

            index++;
            yield return new WaitForSeconds(0.05f);
        }

        DialoguePlaying = false;
    }

    IEnumerator _PlayDialogue(AudioClip clip)
    {
        AfkTime = 0;

        DialoguePlaying = true;
        Audio.PlayOneShot(clip);
        while (Audio.isPlaying)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.05f);
        DialoguePlaying = false;
    }

    IEnumerator _AfkTimer()
    {
        while(true)
        {
            while(DialoguePlaying)
            {
                yield return null;
            }

            AfkTime += Time.deltaTime;
            if (AfkTime > AfkTimer)
            {
                PlayDialogue(AfkClips);
                AfkTime = 0;
            }

            yield return null;
        }
    }
}