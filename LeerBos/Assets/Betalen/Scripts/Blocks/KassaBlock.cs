using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KassaBlock : Block
{
    public GridManager Grid;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void AddCoin(Coin c)
    {
        Grid.AddChange(c.Value);
    }
}
