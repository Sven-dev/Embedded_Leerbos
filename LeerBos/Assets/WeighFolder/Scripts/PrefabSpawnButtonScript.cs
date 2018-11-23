using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawnButtonScript : Interactable
{
    public ManagerScript manager;
    public GameObject prefab;
    
    private AudioSource aSource;
    private int coroutineId = 0;
    private Vector3 defaultScale;

    void Start () {
        aSource = GetComponent<AudioSource>();
        defaultScale = transform.localScale;
    }

    //gets the manager to spawn the button's associated prefab
    protected override void Click(Vector3 clickposition)
    {
        manager.SpawnPrefab(prefab,ScaleHand.Left);
        Reaction();
    }

    private void Reaction()
    {
        aSource.Play();
        coroutineId++;
        StartCoroutine(_reaction());
    }

    private IEnumerator _reaction()
    {
        coroutineId++;
        int id = coroutineId;

        transform.localScale = defaultScale;

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
