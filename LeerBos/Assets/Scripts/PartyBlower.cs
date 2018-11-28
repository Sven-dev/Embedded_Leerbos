using UnityEngine;

public class PartyBlower : Interactable
{
    private AudioSource Audio;

	// Use this for initialization
	void Start ()
    {
        Audio = GetComponent<AudioSource>();
	}

    protected override void Click(Vector3 clickposition)
    {
        Audio.Play();
    }
}