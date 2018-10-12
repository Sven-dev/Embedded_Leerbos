using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawnButtonScript : Interactable
{
    public ManagerScript manager;
    public GameObject prefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //gets the manager to spawn the button's associated prefab
    protected override void Click(Vector3 clickposition)
    {
        manager.SpawnPrefab(prefab);
    }
}
