using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductDeathZone : MonoBehaviour {

    public ProductSpawner Spawner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Deathzone collisionn entered");
        Product p = collision.GetComponent<Product>();
        if(p != null)
        {
            Spawner.ProductClones.Remove(p);
            Destroy(collision.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
