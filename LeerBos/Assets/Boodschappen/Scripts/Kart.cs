using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kart : MonoBehaviour {

    public ProductSpawner ShoppingList;
    [HideInInspector]
    public Transform ProductHolder;
    public AudioSource CorrectSFX;
    public AudioSource IncorrectSFX;

    public float XBoundMin;
    public float XBoundMax;

    private void Start()
    {
        ProductHolder = transform.GetChild(3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Product p = collision.transform.GetComponent<Product>();
        if (p != null)
        {
            if (ShoppingList.ProductCollected(p))
            {
                CorrectSFX.Play();
                p.FallInCart(this);
                return;
            }

            Destroy(p.GetComponent<Collider2D>());
            p.transform.Translate(Vector3.up);
            Rigidbody2D rigidbody = p.GetComponent<Rigidbody2D>();
            rigidbody.AddForce((Vector2.up * 2 + Vector2.left) * 25000);
            rigidbody.AddTorque(35);
            rigidbody.gravityScale = 1;

            IncorrectSFX.Play();
        }
    }

    //Clamps the plane to the upper and right boundaries, meaning the plane can't leave those sides of the screen
    public bool Clamp()
    {
        bool clamped = false;
        if (transform.position.x <= XBoundMin || transform.position.x >= XBoundMax)
        {
            clamped = true;
        }

        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, XBoundMin, XBoundMax),
            transform.position.y);

        return clamped;
    }
}