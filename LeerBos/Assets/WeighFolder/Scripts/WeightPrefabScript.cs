using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightPrefabScript : WeightedObjectScript {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //remove weight from game
    protected override void Click(Vector3 clickposition)
    {
        RemoveFromList();
        Destroy(gameObject);
    }
}
