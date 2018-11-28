using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeightPrefabScript : WeightedObjectScript {

    public AudioClip Delete;

    //remove weight from game
    protected override void Click(Vector3 clickposition)
    {
        RemoveFromList();
        StartCoroutine(_PlaySoundAndDestroy());
    }
    
    IEnumerator _PlaySoundAndDestroy()
    {
        //play pop sound
        aSource.volume = 0.3f;
        aSource.PlayOneShot(Delete);
        //remove visual and gameplay components but dont destroy yet
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Image>().enabled = false;
        GetComponentInChildren<Text>().enabled = false;
        //wait for sound to stop
        while (aSource.isPlaying)
        {
            yield return null;
        }
        //toss that boy
        Destroy(gameObject);
    }
}
