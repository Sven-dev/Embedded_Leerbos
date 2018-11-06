using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    private AudioSource Audio;

	// Use this for initialization
	void Awake ()
    {
        Audio = GetComponent<AudioSource>();
	}

    public void Enable()
    {
        gameObject.SetActive(true);
        Audio.Play();
    }
}