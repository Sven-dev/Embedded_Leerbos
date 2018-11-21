using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kart : MonoBehaviour
{
    public ProductSpawner ShoppingList;
    [Space]
    public AudioClip Correct;
    public AudioClip Incorrect;
    [HideInInspector]
    public Transform ProductHolder;
    private AudioSource Audio;

    private void Start()
    {
        ProductHolder = transform.GetChild(3);
        Audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if the kart caught a product
        Product p = collision.transform.GetComponent<Product>();
        if (p != null)
        {
            //if it's the correct product
            if (ShoppingList.ProductCollected(p))
            {
                Audio.PlayOneShot(Correct);
                p.FallInCart(this);
                return;
            }

            //if it's an incorrect product
            Destroy(p.GetComponent<Collider2D>());
            p.transform.Translate(Vector3.up);
            Rigidbody2D rigidbody = p.GetComponent<Rigidbody2D>();
            rigidbody.AddForce((Vector2.up * 2 + Vector2.left) * 25000);
            rigidbody.AddTorque(35);
            rigidbody.gravityScale = 1;

            Audio.PlayOneShot(Incorrect);
        }
    }
}