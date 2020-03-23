using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawnButtonScript : MonoBehaviour, I_SmartwallInteractable
{
    public ScalesManagerScript manager;
    public GameObject prefab;
    
    private AudioSource aSource;
    private int coroutineId = 0;
    private Vector3 defaultScale;

    void Start ()
    {
        aSource = GetComponent<AudioSource>();
        defaultScale = transform.localScale;
    }

    //gets the manager to spawn the button's associated prefab
    public void Hit(Vector3 clickposition)
    {
        manager.SpawnPrefab(prefab,ScaleHand.Left);
        Reaction();
    }

    private void Reaction()
    {
        //audio feedback
        aSource.Play();
        //visual feedback
        coroutineId++;
        StartCoroutine(_reaction());
    }

    private IEnumerator _reaction()
    {
        int id = coroutineId;

        transform.localScale = defaultScale;

        //shrink, then revert to original size
        //coroutineId ensures the loop ends if id changes
        while (transform.localScale.x > defaultScale.x / 1.1 && id == coroutineId)
        {
            transform.localScale = new Vector2(transform.localScale.x / 1.02f, transform.localScale.y / 1.02f);
            yield return null;
        }
        while (transform.localScale.x < defaultScale.x && id == coroutineId)
        {
            transform.localScale = new Vector2(transform.localScale.x * 1.02f, transform.localScale.y * 1.02f);
            yield return null;
        }
    }
}
