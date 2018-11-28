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
        aSource.volume = 0.3f;
        aSource.PlayOneShot(Delete);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Image>().enabled = false;
        GetComponentInChildren<Text>().enabled = false;
        while (aSource.isPlaying)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
