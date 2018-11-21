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

    // Use this for initialization
    void Start () {
        trans = GetComponent<RectTransform>();
        aSource = GetComponent<AudioSource>();
        coroutineId = 0;
	}
	
	// Update is called once per frame
	void Update () {

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
        StartCoroutine(_reaction(coroutineId));
    }

    private IEnumerator _reaction(int id)
    {
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
