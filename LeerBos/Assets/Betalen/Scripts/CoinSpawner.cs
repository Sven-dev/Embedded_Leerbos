using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public bool Active;
    public float Cooldown;

    public List<Coin> Prefabs;
    public Transform Belt;

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
        Coin prefab = Prefabs[Random.Range(0, Prefabs.Count)];
        Vector3 location = transform.GetChild(Random.Range(0, transform.childCount)).position;

        Coin clone = Instantiate(prefab, Belt);
        clone.transform.position = location;
    } 
}