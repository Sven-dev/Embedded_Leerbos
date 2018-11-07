using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : Interactable
{
    private AudioSource Audio;
    private bool DialoguePlaying;
    private int Index;

    [Space]
    public AudioClip HitClip;
    public List<AudioClip> IntroClips;
    public List<AudioClip> VictoryClips;

    // Use this for initialization
    void Start ()
    {
        DialoguePlaying = false;
        Audio = GetComponent<AudioSource>();
	}

    public void PlayDialogue(List<AudioClip> clips)
    {
        StartCoroutine(_PlayDialogue(clips));
    }

    public void PlayDialogue(AudioClip clip)
    {
        StartCoroutine(_PlayDialogue(clip));
    }

    protected override void Click(Vector3 clickposition)
    {
        /*
        print("Click");
        if (DialoguePlaying)
        {
            print("Dialogue skip");
            Audio.Stop();
        }
        else
        {
            print("Hit");
            StartCoroutine(_PlayHit());
        }
        */
    }

    IEnumerator _PlayDialogue(List<AudioClip> clips)
    {
        Index = 0;
        DialoguePlaying = true;
        while (Index < clips.Count)
        {
            Audio.PlayOneShot(clips[Index]);
            while (Audio.isPlaying)
            {
                yield return null;
            }

            Index++;
            yield return new WaitForSeconds(1f);
        }

        DialoguePlaying = false;
    }

    IEnumerator _PlayDialogue(AudioClip clip)
    {
        DialoguePlaying = true;
        Audio.PlayOneShot(clip);
        while (Audio.isPlaying)
        {
            yield return null;
        }

        DialoguePlaying = false;
    }
}