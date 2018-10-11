using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingListManager : MonoBehaviour {

    public Text CurrentItem;
    public Text NextItem;
    public GameObject VictoryCanvas;

    ProductSpawner Spawner;

    // Use this for initialization
    void Awake ()
    {
        CurrentItem = transform.GetChild(0).GetComponent<Text>();
        NextItem = transform.GetChild(1).GetComponent<Text>();
    }
	
    public void Link(ProductSpawner spawner)
    {
        Spawner = spawner;
        Spawner.ShoppingListChange += UpdateShoppingList;
        StartCoroutine(_ShowNextItem());
    }

    void UpdateShoppingList()
    {
        StartCoroutine(_RemoveCurrentItem());
    }

    IEnumerator _RemoveCurrentItem()
    {
        while (CurrentItem.color.a > 0)
        {
            CurrentItem.color = new Color(0,0,0, CurrentItem.color.a - 0.05f);
            yield return null;
        }

        CurrentItem.text = "";
        CurrentItem.color = new Color(0, 0, 0, 1);

        if (Spawner.Products.Count > 0)
        {
            StartCoroutine(_ShowNextItem());
        }
        else
        {
            Spawner.Victory();
            yield return new WaitForSeconds(1);
            VictoryCanvas.SetActive(true);
        }
        
    }

    IEnumerator _ShowNextItem()
    {
        CurrentItem.text = "";
        NextItem.text = Spawner.Products[0];
        Vector3 NextItemPosition = NextItem.transform.position;

        //Move nextitem to currentitem
        while (NextItem.transform.localPosition.y > CurrentItem.transform.localPosition.y)
        {
            NextItem.transform.Translate(Vector3.down * Time.deltaTime, transform);
            yield return null;
        }

        //Replace currentitem with nextitem & move nextitem back
        CurrentItem.text = NextItem.text;
        NextItem.transform.position = NextItemPosition;
    }
}