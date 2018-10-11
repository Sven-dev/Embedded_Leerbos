﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kart : MonoBehaviour {

    public ProductSpawner ShoppingList;
    public Transform ProductHolder;

    public float Speed;

    private Vector2 TargetPosition;

    private void Start()
    {
        ProductHolder = transform.GetChild(2);
    }

    public void Move(Vector3 position)
    {
        position.y = transform.position.y;
        TargetPosition = position;
        StartCoroutine(_Move());
    }

    IEnumerator _Move()
    {
        Vector2 currentmove = TargetPosition;
        while (transform.position.x != TargetPosition.x && currentmove == TargetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, TargetPosition, Speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Kart collision entered");
        Product p = collision.transform.GetComponent<Product>();
        if (p != null)
        {
            print(1);
            if (ShoppingList.ProductCollected(p))
            {
                print(2);
                p.transform.parent = transform;
                p.FallInCart(this);
                return;
            }

            print("adding force");
            p.GetComponent<Rigidbody2D>().AddForce((Vector2.up + Vector2.left) * 500);
            //p.GetComponent<Rigidbody2D>().AddRelativeForce()
        }       
    }
}