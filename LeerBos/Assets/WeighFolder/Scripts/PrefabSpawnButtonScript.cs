using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawnButtonScript : Interactable
{
    public ManagerScript manager;
    public GameObject prefab;

    private RectTransform trans;
    private AudioSource aSource;
    private int coroutineId;

    void Start () {
        trans = GetComponent<RectTransform>();
        aSource = GetComponent<AudioSource>();
        coroutineId = 0;
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

        trans.sizeDelta = new Vector2(100,100);

        while (trans.sizeDelta.x < 110 && id == coroutineId)
        {
            trans.sizeDelta = new Vector2(trans.sizeDelta.x + 2, trans.sizeDelta.y + 2);
            yield return null;
        }
        while (trans.sizeDelta.x > 100 && id == coroutineId)
        {
            trans.sizeDelta = new Vector2(trans.sizeDelta.x - 2, trans.sizeDelta.y - 2);
            yield return null;
        }
    }
}
