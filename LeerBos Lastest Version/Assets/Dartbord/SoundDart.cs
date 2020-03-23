using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundDart : MonoBehaviour {
    [SerializeField] GameRules controller;
    [SerializeField] GameRulesV2 controllerV2;
    [SerializeField] public AudioSource Audio;
    [SerializeField] public AudioSource Audio2;
    [SerializeField] public AudioClip[] HitSounds;
    [SerializeField] public AudioClip Correct;
    [SerializeField] public AudioClip False;
    [SerializeField] public AudioClip DoorOpen;
    [SerializeField] public AudioClip[] LockOpenSounds;

    // Use this for initialization
    public void SoundPlay() {
        StartCoroutine(_HitSound());
	}
    public IEnumerator _HitSound()
    {
        int soundSize;
        if(SceneManager.GetActiveScene().name == "DartenV2")
        {
            soundSize = controllerV2.Score;
        }
        else
        {
            soundSize = controller.Score;

        }
        AudioClip myClip;
        switch (soundSize)
        {
           case 0:
                myClip = HitSounds[0];
                Audio.clip = myClip;
                Debug.Log(myClip.ToString());
                Audio.Play();
                break;
            case 1:
                myClip = HitSounds[1];
                Audio.clip = myClip;
                Audio.Play();
                break;
            case 2:
                myClip = HitSounds[2];
                Audio.clip = myClip;
                Audio.Play();
                break;
            case 3:
                myClip = HitSounds[3];
                Audio.clip = myClip;
                Audio.Play();
                break;
            case 4:
                myClip = HitSounds[3];
                Audio.clip = myClip;
                Audio.Play();
                break;

        }
        yield return null;
    }
    public void CorrectSound()
    {
        AudioClip myClip;
        myClip = Correct;
        Audio.clip = myClip;
        Audio.Play();
    }
    public void FalseSound()
    {
        AudioClip myClip;
        myClip = False;
        Audio.clip = myClip;
        Audio.Play();
    }
    public void OpenSound()
    {
        AudioClip myClip;
        myClip = DoorOpen;
        Audio.clip = myClip;
        Audio.Play();
    }
    public void LockOpenSound()
    {
        AudioClip myClip;
        myClip = LockOpenSounds[Random.Range(0,2)];
        Audio2.clip = myClip;
        Audio2.Play();
    }
}
