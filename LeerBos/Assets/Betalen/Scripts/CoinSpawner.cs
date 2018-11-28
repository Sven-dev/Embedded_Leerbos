using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public bool Active;
    public float Cooldown;

    [Space]
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
            SpawnCoin();
            yield return new WaitForSeconds(Cooldown);
        }
    }

    //Instantiates a random coin from Prefabs by combining the spawn-chance from each item
    void SpawnCoin()
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
        else //if (rnd < Prefabs[0].SpawnMultiplier + Prefabs[1].SpawnMultiplier + Prefabs[2].SpawnMultiplier + Prefabs[3].SpawnMultiplier)
        {
            prefab = Prefabs[3];
        }

        Coin clone = Instantiate(prefab, Belt.CoinHolder);
        //Set the object's position to one of the spawn-locations
        clone.transform.position = transform.GetChild(Random.Range(0, transform.childCount)).position;
    } 
}