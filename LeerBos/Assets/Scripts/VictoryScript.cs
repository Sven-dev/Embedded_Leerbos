using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    public delegate void Victory();
    public static event Victory OnVictory;

    private AudioSource Audio;
    public Saveable VictoryState;

	// Use this for initialization
	void Awake ()
    {
        Audio = GetComponent<AudioSource>();
	}

    public void Enable()
    {
        OnVictory();
        gameObject.SetActive(true);
        VictoryState.Set(true);
        Audio.Play();
    }
}