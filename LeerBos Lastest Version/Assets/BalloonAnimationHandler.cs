using System.Collections;
using System.Collections.Generic;
using Balloons;
using UnityEngine;

public class BalloonAnimationHandler : MonoBehaviour {
    [SerializeField] AudioClip Popsound;

    public void Delete()
	{
		GetComponentInParent<Balloon>().Delete();
	}
    public void PopSound()
    { 
    GetComponentInParent<AudioSource>().clip = Popsound;
    GetComponentInParent<AudioSource>().Play();
    }
}
