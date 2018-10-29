using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public float Value;

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ConveyorBelt")
        {
            Block b = collision.transform.parent.GetComponent<Block>();
            if (b != null)
            {
                b.AddCoin(this);
            }
        }
    }
}