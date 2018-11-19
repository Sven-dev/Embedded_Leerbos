using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Product : Interactable
{
    private Rigidbody2D Rigidbody;
    private Collider2D Collider;
    private Text Label;
    [HideInInspector]
    public AudioSource NameSFX;
    bool IncreasedGravity;

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
        NameSFX = GetComponent<AudioSource>();

        IncreasedGravity = false;
	}

    public void FallInCart(Kart k)
    {
        Destroy(Rigidbody);
        Destroy(Collider);
        transform.SetParent(k.ProductHolder);
    }

    public void SetProduct(string name)
    {
        ProductName = name;
    }

    private void IncreaseGravity(int amount)
    {
        if (!IncreasedGravity)
        {
            Rigidbody.gravityScale *= amount;
            IncreasedGravity = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IncreaseGravity(4);
    }

    protected override void Click(Vector3 clickposition)
    {
        IncreaseGravity(50);
    }
}