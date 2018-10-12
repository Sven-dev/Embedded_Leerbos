using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    public GameObject WeightParent;
    public ScaleHandScript LeftHand;
    public WeightedObjectScript ObjectToWeigh;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //spawn the submitted prefab above the left scale
    public void SpawnPrefab(GameObject prefab)
    {
        Instantiate(prefab, new Vector2(GetLeftHandX(), 10),Quaternion.Euler(0,0,0),WeightParent.transform);
    }

    //get x position of the left scale
    float GetLeftHandX()
    {
        return LeftHand.gameObject.transform.position.x;
    }
}
