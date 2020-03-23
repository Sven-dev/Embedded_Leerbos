using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : SceneSwitchable, I_SmartwallInteractable
{
    private AudioSource Audio;

    private void Start()
    {
        //Audio = GetComponent<AudioSource>();
    }

    public void Hit(Vector3 hitPosition)
    {
        if (TargetString != "")
        {
            base.Hit(hitPosition);
        }
        else
        {
            Audio.Play();
        }
    }
}