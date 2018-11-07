using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : Interactable
{
    public List<AudioClip> VoiceClips;
    public AudioClip HitClip;
    private AudioSource Audio;

    private bool DialoguePlaying;
    private int Index;

	// Use this for initialization
	void Start ()
    {
        DialoguePlaying = false;

        Audio = GetComponent<AudioSource>();
        StartCoroutine(_PlayDialogue());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void Click(Vector3 clickposition)
    {
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
    }

    IEnumerator _PlayHit()
    {
        Audio.PlayOneShot(HitClip);
        while (Audio.isPlaying)
        {
            yield return null;
        }      
    }

    IEnumerator _PlayDialogue()
    {
        yield return new WaitForSeconds(5f);

        Index = 0;
        DialoguePlaying = true;
        while (Index < VoiceClips.Count)
        {
            Audio.PlayOneShot(VoiceClips[Index]);
            while (Audio.isPlaying)
            {
                yield return null;
            }

            Index++;
            yield return new WaitForSeconds(1f);
        }

        DialoguePlaying = false;
    }
}