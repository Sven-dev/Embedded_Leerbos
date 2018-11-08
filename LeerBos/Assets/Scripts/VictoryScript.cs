using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    private AudioSource Audio;
    public Saveable VictoryState;

	// Use this for initialization
	void Awake ()
    {
        Audio = GetComponent<AudioSource>();
	}

    public void Enable()
    {
        gameObject.SetActive(true);
        VictoryState.Set(true);
        Audio.Play();
    }
}