using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitBlock : Block
{
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void AddCoin(Coin c)
    {
        c.transform.parent = transform;
        StartCoroutine(_Fall(c));
    }

    IEnumerator _Fall(Coin c)
    {
        while (c.transform.localScale.x > 0)
        {
            c.transform.localScale -= Vector3.one * 0.25f;
            yield return null;
        }

        Destroy(c.gameObject);
    }

    protected override void Click(Vector3 clickposition)
    {
        //base.Click(clickposition);
    }
}
