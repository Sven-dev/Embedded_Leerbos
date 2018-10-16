using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Product : MonoBehaviour {

    private Rigidbody2D Rigidbody;
    private Collider2D Collider;
    private Text Label;

    public string ProductName
    {
        get { return Label.text; }
        set { Label.text = value; }
    }

	// Use this for initialization
	void Awake ()
    {
        Label = transform.GetChild(0).GetComponent<Text>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
	}

    public void FallInCart(Kart k)
    {
        Destroy(Rigidbody);
        Destroy(Collider);
        transform.parent = k.ProductHolder;
    }

    public void SetProduct(string name)
    {
        ProductName = name;
    }
}