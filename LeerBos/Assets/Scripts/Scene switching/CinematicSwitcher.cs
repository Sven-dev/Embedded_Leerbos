using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CinematicSwitcher : SceneSwitchable
{
    VideoPlayer Player;

	// Use this for initialization
	void Start ()
    {
        Player = GetComponent<VideoPlayer>();
        StartCoroutine(_PlayIntro());
	}

    IEnumerator _PlayIntro()
    {
        yield return new WaitForSeconds(1);
        bool playing = true;
        while(playing)
        {
            if (!Player.isPlaying)
            {
                playing = false;
            }

            yield return null;
        }

        Switch();
    }
}