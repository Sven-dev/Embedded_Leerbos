using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightPrefabScript : WeightedObjectScript {
	
    //remove weight from game
    protected override void Click(Vector3 clickposition)
    {
        RemoveFromList();
        Destroy(gameObject);
    }
}
