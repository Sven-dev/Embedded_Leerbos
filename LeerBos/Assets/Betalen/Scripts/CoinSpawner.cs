using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public bool Active;
    public float Cooldown;

    public List<Coin> Prefabs;
    public ConveyorBelt Belt;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(_Run());
	}
	
    IEnumerator _Run()
    {
        Active = true;
        while (Active)
        {
            SpawnCoins();
            yield return new WaitForSeconds(Cooldown);
        }
    }

    void SpawnCoins()
    {
        int rndmax = 0;
        foreach (Coin c in Prefabs)
        {
            rndmax += c.SpawnMultiplier;
        }

        int rnd = Random.Range(0, rndmax);

        Coin prefab;
        if (rnd < Prefabs[0].SpawnMultiplier)
        {
            prefab = Prefabs[0];
        }
        else if (rnd < Prefabs[0].SpawnMultiplier + Prefabs[1].SpawnMultiplier)
        {
            prefab = Prefabs[1];
        }
        else if (rnd < Prefabs[0].SpawnMultiplier + Prefabs[1].SpawnMultiplier + Prefabs[2].SpawnMultiplier)
        {
            prefab = Prefabs[2];
        }
        else if (rnd < Prefabs[0].SpawnMultiplier + Prefabs[1].SpawnMultiplier + Prefabs[2].SpawnMultiplier + Prefabs[3].SpawnMultiplier)
        {
            prefab = Prefabs[3];
        }
        else if (rnd < Prefabs[0].SpawnMultiplier + Prefabs[1].SpawnMultiplier + Prefabs[2].SpawnMultiplier + Prefabs[3].SpawnMultiplier + Prefabs[4].SpawnMultiplier)
        {
            prefab = Prefabs[4];
        }
        else if (rnd < Prefabs[0].SpawnMultiplier + Prefabs[1].SpawnMultiplier + Prefabs[2].SpawnMultiplier + Prefabs[3].SpawnMultiplier + Prefabs[4].SpawnMultiplier + Prefabs[5].SpawnMultiplier)
        {
            prefab = Prefabs[5];
        }
        else //if (rndmax < Prefabs[0].SpawnMultiplier + Prefabs[1].SpawnMultiplier + Prefabs[2].SpawnMultiplier + Prefabs[3].SpawnMultiplier + Prefabs[4].SpawnMultiplier + Prefabs[5].SpawnMultiplier + Prefabs[6].SpawnMultiplier )
        {
            prefab = Prefabs[6];
        }

        Vector3 location = transform.GetChild(Random.Range(0, transform.childCount)).position;

        Coin clone = Instantiate(prefab, Belt.CoinHolder);
        clone.transform.position = location;
    } 
}