using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour, I_SmartwallInteractable
{
    private AudioSource Audio;
    public bool DialoguePlaying;
    public AfkTimer IdleTimer;

    [Space]
    public AudioClip HitClip;
    public List<AudioClip> IntroClips;
    public List<AudioClip> VictoryClips;

    [Space]
    public float AfkTimer;

    [Space]
    public SceneSwitcher SceneSwitcher;

    // Use this for initialization
    void Start ()
    {
        DialoguePlaying = false;
        Audio = GetComponent<AudioSource>();
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

    public void Hit(Vector3 clickposition)
    {
        if (!DialoguePlaying)
        {
            PlayDialogue(HitClip);
        }
    }

    IEnumerator _PlayDialogue(List<AudioClip> clips)
    {
        int index = 0;
        DialoguePlaying = true;
        if (IdleTimer != null)
        {
            IdleTimer.Active = false;
        }

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

        //if game is complete, switch to the outro (this is ugly, I know)
        if (name == "Mouse" && clips == VictoryClips)
        {
            SceneSwitcher.Switch("Outro", null);
        }

        if (IdleTimer != null)
        {
            IdleTimer.Active = true;
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

        yield return new WaitForSeconds(0.05f);
        DialoguePlaying = false;
    }
}