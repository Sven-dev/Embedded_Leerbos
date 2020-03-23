using UnityEngine;

public class PartyBlower : MonoBehaviour, I_SmartwallInteractable
{
    private AudioSource Audio;

	// Use this for initialization
	void Start ()
    {
        Audio = GetComponent<AudioSource>();
	}

    public void Hit(Vector3 clickposition)
    {
        Audio.Play();
    }
}