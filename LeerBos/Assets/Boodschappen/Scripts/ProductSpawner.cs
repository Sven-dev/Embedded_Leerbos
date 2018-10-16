using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductSpawner : MonoBehaviour {

    public int ProductsToGo;
    [Space]
    public int SpawnAmount;
    public List<Transform> SpawnLocations;
    [Space]
    public List<string> Products;
    public List<Product> ProductPrefabs;

    [HideInInspector]
    public List<Product> ProductClones;

    public delegate void ShoppingListChanged();
    public event ShoppingListChanged ShoppingListChange;

    public ShoppingListManager Shoppinglist;
    private GrammarManager GrammarManager;

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
        GrammarManager = GetComponent<GrammarManager>();
        StartCoroutine(Loop());
	}

    IEnumerator Loop()
    {
        while (ProductsToGo > 0)
        {
            while (ProductClones.Count > 0)
            {
                yield return null;
            }

            if (Products.Count > 0)
            {
                SpawnItems();
            }

            yield return null;
        }
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
        //get random spawn-locations
        List<Transform> locations = GetSpawnLocations();

        //Ggenerate the correct answer & a number of misspellings
        List<string> words = GrammarManager.MessUpGrammar(Products[0], SpawnAmount);
        //Instantiate the products
        for (int i = 0; i < words.Count; i++)
        {
            SpawnItem(locations[i], ProductPrefabs[0], words[i]);
        }
    }

    //spawns 3 products, 1 spelled correctly and 2 with scrambled letters
    void SpawnItem(Transform spawn, Product prefab, string text)
    {
        Product p = Instantiate(prefab, spawn);
        ProductClones.Add(p);
        p.SetProduct(text);
    }

    public bool ProductCollected(Product product)
    {
        print(Products[0]);
        print(product.ProductName);
        if (Products[0] == product.ProductName)
        {
            Products.RemoveAt(0);
            ProductPrefabs.RemoveAt(0);
            ProductClones.RemoveAt(0);
            ShoppingListChange();
            return true;
        }

        return false;
    }
}