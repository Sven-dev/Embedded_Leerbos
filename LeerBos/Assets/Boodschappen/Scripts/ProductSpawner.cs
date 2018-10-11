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

    [HideInInspector]
    public List<Product> ProductClones;

    public delegate void ShoppingListChanged();
    public event ShoppingListChanged ShoppingListChange;
    public ShoppingListManager Shoppinglist;

    // Use this for initialization
    void Start ()
    {
        SpawnLocations = new List<Transform>();
        foreach (Transform child in transform)
        {
            SpawnLocations.Add(child);
        }

        Shoppinglist.Link(this);
        ProductClones = new List<Product>();
        StartCoroutine(Loop());
	}

    IEnumerator Loop()
    {
        while (Active)
        {
            SpawnItems();
            while (ProductClones.Count > 0)
            {
                yield return null;
            }

            yield return null;
        }
    }

    //Scrambles the letters in a word
    string Scramble(string word)
    {
        string scrambledword = "";
        for (int i = 0; i < word.Length; i++)
        {
            int rnd = Random.Range(0, word.Length -1);

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
            Transform templocation = SpawnLocations[Random.Range(0, SpawnLocations.Count)];
            if (!templocations.Contains(templocation))
            {
                templocations.Add(templocation);
            }
        }

        return templocations;
    }

    //Spawns a set of items
    void SpawnItems()
    {
        Product prefab = ProductPrefabs[Random.Range(0, ProductPrefabs.Count - 1)];
        string text = Products[0];

        List<Transform> locations = GetSpawnLocations();
        SpawnItem(locations[0], prefab, text);
        for (int i = 1; i < SpawnAmount; i++)
        {
            SpawnItem(locations[i], prefab, Scramble(text));
        }
    }

    //spawns 3 products, 1 spelled correctly and 2 with scrambled letters
    void SpawnItem(Transform spawn, Product prefab, string text)
    {
        Product p = Instantiate(prefab, spawn);
        ProductClones.Add(p);
        p.ProductName = text;
    }

    public bool ProductCollected(Product product)
    {
        if (Products[0] == product.ProductName)
        {
            Products.RemoveAt(0);
            ProductClones.Remove(product);
            ShoppingListChange();
            return true;
        }

        return false;
    }

    public void Victory()
    {
        Active = false;
    }
}