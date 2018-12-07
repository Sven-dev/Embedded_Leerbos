using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : SceneSwitchable
{
    private AudioSource Audio;

    private void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    protected override void Click(Vector3 clickposition)
    {
        if (TargetString != "")
        {
            base.Click(clickposition);
        }
        else
        {
            Audio.Play();
        }
    }
}