using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductSpawner : MonoBehaviour {

    public string CorrectProduct;
    [Space]
    public int SpawnAmount;
    public List<Transform> SpawnLocations;
    [Space]
    public List<Product> ProductPrefabs;

    [HideInInspector]
    public List<Product> ProductClones;

    public delegate void ShoppingListChanged();
    public event ShoppingListChanged ShoppingListChange;
    private bool CorrectItemWait;

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
        CorrectItemWait = false;
        ProductClones = new List<Product>();
        GrammarManager = GetComponent<GrammarManager>();
        StartCoroutine(Loop());
	}

    IEnumerator Loop()
    {
        while (ProductPrefabs.Count > 0)
        {
            SpawnItems();
            while (ProductClones.Count > 0 || CorrectItemWait)
            {
                yield return null;
            }
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

        //Instantiate the products
        for (int i = 0; i < SpawnAmount; i++)
        {
            SpawnItem(locations[i], ProductPrefabs[0]);
        }

        //Generate the correct answer & a number of misspellings
        List<string> words = GrammarManager.MessUpGrammar(ProductClones[0].ProductName, SpawnAmount);

        for (int i = 0; i < SpawnAmount; i++)
        {
            ProductClones[i].SetProduct(words[i]);
        }

        CorrectProduct = ProductClones[0].ProductName;
        ProductClones[0].NameAudio.Play();
    }

    //spawns 3 products, 1 spelled correctly and 2 with scrambled letters
    void SpawnItem(Transform spawn, Product prefab)
    {
        float rnd = Random.Range(0f, 1f);
        Product p = Instantiate(prefab, spawn);
        p.transform.position = new Vector3(spawn.position.x, spawn.position.y + rnd, spawn.position.z);

        ProductClones.Add(p);
    }

    public bool ProductCollected(Product product)
    {
        if (ProductClones.Count == 0)
        {
            return false;
        }
        else if (product.ProductName == CorrectProduct)
        {
            ProductPrefabs.RemoveAt(0);
            ProductClones.Clear();
            StartCoroutine(_CorrectItem());
            ShoppingListChange();
            return true;
        }

        return false;
    }

    IEnumerator _CorrectItem()
    {
        CorrectItemWait = true;
        yield return new WaitForSeconds(1);
        CorrectItemWait = false;
    }
}