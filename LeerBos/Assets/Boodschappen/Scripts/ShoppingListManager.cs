using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingListManager : MonoBehaviour
{
    public GameObject VictoryCanvas;

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
        Spawner.ShoppingListChange += UpdateShoppingList;
        spawner.OnVictory += Victory;
        StartCoroutine(_ShowNextItem());
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
        VictoryCanvas.SetActive(true);
    }

    IEnumerator _RemoveCurrentItem()
    {
        print("removecurrentitem");
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
        currentitem.transform.localScale = new Vector3(1, 1, 1);
        Items.Remove(currentitem);

        if (Spawner.Products.Count > 0)
        {
            StartCoroutine(_ShowNextItem());
        }      
    }

    IEnumerator _ShowNextItem()
    {
        yield return new WaitForSeconds(0.5f);
        Image currentitem = Items[0];
        while (currentitem.transform.localScale.x < 1.25f)
        {
            //currentitem.transform.localScale += new Vector3(0.05f, 0.05f);
            currentitem.transform.localScale += new Vector3(0.05f, 0.05f);
            yield return null;
        }

        currentitem.transform.localScale = new Vector3(1.25f, 1.25f, 1);
    }
}