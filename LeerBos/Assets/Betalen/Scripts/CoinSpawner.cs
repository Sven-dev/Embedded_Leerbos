using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : Interactable
{
    [HideInInspector]
    public Connector Connector;

    public Transform CoinHolder;
    public Coin Coin;

    private void Start()
    {
        Connector = transform.GetComponentInChildren<Connector>();
    }

    protected override void Click(Vector3 clickposition)
    {
        SpawnCoin();
    }

    void SpawnCoin()
    {
        Coin c = Instantiate(Coin, transform.position, Quaternion.identity, CoinHolder);
        c.transform.Translate(Vector3.up);
        c.transform.Translate(Vector3.forward);
        c.transform.Translate(Vector3.back);
    }
}
