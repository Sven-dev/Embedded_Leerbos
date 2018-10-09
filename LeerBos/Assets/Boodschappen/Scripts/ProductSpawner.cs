using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductSpawner : MonoBehaviour {

    public bool Active;
    [Space]
    public int SpawnAmount;
    private List<Transform> SpawnLocations;
    [Space]
    public List<string> Products;
    public List<Product> ProductPrefabs;

	// Use this for initialization
	void Start ()
    {
        SpawnLocations = new List<Transform>();
        foreach (Transform child in transform)
        {
            SpawnLocations.Add(child);
        }

        StartCoroutine(Loop());
	}

    IEnumerator Loop()
    {
        while (Active)
        {
            yield return new WaitForSeconds(1);

            Product prefab = ProductPrefabs[Random.Range(0, ProductPrefabs.Count - 1)];
            string text = Products[Random.Range(0, Products.Count - 1)];

            List<Transform> locations = GetSpawnLocations();
            SpawnItem(locations[0], prefab, text);
            for (int i = 1; i < SpawnAmount; i++)
            {
                SpawnItem(locations[i], prefab, Scramble(text));
            }   
        }
    }

    string Scramble(string word)
    {
        string scrambledword = "";
        for (int i = 0; i < word.Length; i++)
        {
            int rnd = Random.Range(i, word.Length -1);

            scrambledword += word.Substring(rnd, 1);
        }

        return scrambledword;
    }

    //Gets a number of random locations, the amount based on SpawnAmount.
    List<Transform> GetSpawnLocations()
    {
        List<Transform> templocations = new List<Transform>();
        while(templocations.Count < SpawnAmount)
        {
            Transform templocation = SpawnLocations[Random.Range(0, SpawnLocations.Count - 1)];
            if (!templocations.Contains(templocation))
            {
                templocations.Add(templocation);
            }
        }

        return templocations;
    }

    //spawns 3 products, 1 spelled correctly and 2 with scrambled letters
    void SpawnItem(Transform spawn, Product prefab, string text)
    {
        print("spawned");
        Instantiate(prefab, spawn);
    }
}
