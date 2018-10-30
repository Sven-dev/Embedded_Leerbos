using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : Block
{
    public Coin Coin;

    protected override void Click(Vector3 clickposition)
    {
        SpawnCoin();
    }

    void SpawnCoin()
    {
        Coin c = Instantiate(Coin, transform.position, Quaternion.identity, transform);
        c.transform.Translate(Vector3.up * 1.9f);
        c.transform.Translate(Vector3.forward);
        c.transform.Translate(Vector3.back);
    }
}
