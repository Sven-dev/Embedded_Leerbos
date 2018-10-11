using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Product : MonoBehaviour {

    public string ProductName;
    private Rigidbody2D Rigidbody;
    private Collider2D Collider;


	// Use this for initialization
	void Start ()
    {
        GetComponentInChildren<Text>().text = ProductName;
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
	}

    public void FallInCart(Kart k)
    {
        Destroy(Rigidbody);
        Destroy(Collider);
        transform.parent = k.ProductHolder;
    }
}