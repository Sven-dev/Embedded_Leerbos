using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingListManager : MonoBehaviour
{
    public VictoryScript VictoryCanvas;

    List<Image> Items;
    ProductSpawner Spawner;

    // Use this for initialization
    void Awake ()
    {
        Items = new List<Image>();
        Items.AddRange(GetComponentsInChildren<Image>());
        Items.RemoveAt(0);
    }
	
    public void Link(ProductSpawner spawner)
    {
        Spawner = spawner;
        PopulateList();
        Spawner.ShoppingListChange += UpdateShoppingList;
        StartCoroutine(_ShowNextItem());
    }

    //Gets the shopping items from spawner and adds them to the list
    void PopulateList()
    {
        for(int i = 0; i < Spawner.ProductPrefabs.Count || i < Items.Count; i++)
        {
            Image source = Spawner.ProductPrefabs[i].GetComponent<Image>();
            Items[i].sprite = source.sprite;
            Items[i].SetNativeSize();
            Items[i].color = new Color(source.color.r, source.color.g, source.color.b, 0.5f);
        }
    }

    void UpdateShoppingList()
    {
        StartCoroutine(_RemoveCurrentItem());
    }

    void Victory()
    {
        StartCoroutine(_Victory());
    }

    IEnumerator _Victory()
    {
        yield return new WaitForSeconds(1);
        VictoryCanvas.Enable();
    }

    IEnumerator _RemoveCurrentItem()
    {
        Image currentitem = Items[0];
        while (currentitem.color.a < 1)
        {
            currentitem.color = new Color(
                currentitem.color.r,
                currentitem.color.g,
                currentitem.color.b, 
                currentitem.color.a + 0.05f);
            yield return null;
        }

        yield return new WaitForSeconds(0.75f);
        currentitem.transform.localScale = new Vector3(.15f, .15f, 1);
        Items.Remove(currentitem);

        if (Spawner.ProductPrefabs.Count > 0)
        {
            StartCoroutine(_ShowNextItem());
        }  
        else
        {
            Victory();
        }
    }

    IEnumerator _ShowNextItem()
    {
        Image currentitem = Items[0];
        float targetscale = currentitem.transform.localScale.x * 1.5f;
        yield return new WaitForSeconds(0.5f);

        float percentage = (targetscale - currentitem.transform.localScale.x) / 100 * 25;
        while (currentitem.transform.localScale.x < targetscale)
        {
            currentitem.transform.localScale += new Vector3(percentage, percentage);
            yield return null;
        }

        currentitem.transform.localScale = new Vector3(targetscale, targetscale, 1);
    }
}