using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductDeathZone : MonoBehaviour {

    public ProductSpawner Spawner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Product p = collision.GetComponent<Product>();
        if(p != null)
        {
            Spawner.ProductClones.Remove(p);
            Destroy(collision.gameObject);
        }
    }
}
