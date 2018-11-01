using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float Speed;

	// Use this for initialization
	void Awake ()
    {
        StartCoroutine(_Move());
	}

    IEnumerator _Move()
    {
        yield return new WaitForSeconds(2);
        while (true)
        {
            foreach (Transform child in transform)
            {
                child.Translate(transform.right * Speed * Time.deltaTime);
            }

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Coin c = collision.transform.parent.GetComponent<Coin>();
        if (c != null)
        {
            c.transform.SetParent(transform, true);
            c.Moving = false;
        }
    }
}