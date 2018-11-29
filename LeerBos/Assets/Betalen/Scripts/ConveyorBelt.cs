using System.Collections;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float Speed;
    [HideInInspector]
    public Transform CoinHolder;
    private Transform Belt;

	// Use this for initialization
	void Awake ()
    {
        CoinHolder = transform.GetChild(0);
        Belt = transform.GetChild(1);
        StartCoroutine(_Move());
	}

    IEnumerator _Move()
    {
        while (true)
        {
            //Move all coins
            foreach (Transform child in CoinHolder)
            {
                child.Translate(transform.right * Speed * Time.deltaTime);
            }

            //Move the belt objects
            foreach (RectTransform child in Belt)
            {
                if (child.position.x > 13.75f)
                {
                    child.position = new Vector3(-14.55f, child.position.y, child.position.z);
                }

                child.transform.Translate(Vector3.right * Speed * Time.deltaTime);
            }

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Coin c = collision.transform.parent.GetComponent<Coin>();
        if (c != null && !c.Register.CorrectAnswer && c.transform.parent.tag == "CashRegister")
        {
            c.transform.SetParent(CoinHolder, true);
            c.Register.Compare();
        }
    }
}