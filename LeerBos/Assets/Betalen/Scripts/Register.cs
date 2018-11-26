using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Register : MonoBehaviour
{
    public int Rounds;
    public double Price;

    public Text PriceLabel;
    public Transform Closed;
    public Transform Opened;

    private Transform Drawer;

    public delegate void PriceChange(double price);
    public static event PriceChange OnPriceChange;
    public UIPrice UILabel;
    public VictoryScript VictoryLabel;
    private bool Victory;

    private AudioSource Audio;

    private void Start()
    {
        Victory = false;
        Audio = GetComponent<AudioSource>();
        StartCoroutine(_GeneratePrice());

        Drawer = transform.GetChild(0);
    }

    //Compares the required price to the sum of all coint that are children of the counter
    private void Compare()
    {
        if (!Victory)
        {
            double coinstotal = 0;
            foreach (Coin c in transform.GetComponentsInChildren<Coin>())
            {
                coinstotal += c.Value;
            }

            if (Math.Round(coinstotal, 2) == Math.Round(Price, 2))
            {
                StartCoroutine(_CorrectPayment());
            }
        }
    }

    private void Empty()
    {
        foreach (Coin coin in transform.GetComponentsInChildren<Coin>())
        {
            Destroy(coin.gameObject);
        }
    }

    //Show's the victory-screen or generates a new price and moves the coins off-screen
    IEnumerator _CorrectPayment()
    {
        Rounds--;
        if (Rounds == 0)
        {
            StartCoroutine(_Victory());
        }
        else
        {
            StartCoroutine(_GeneratePrice());
        }

        yield return new WaitForSeconds(1);

        Audio.Play();

        while (Drawer.position != Closed.position)
        {
            Drawer.position = Vector3.MoveTowards(Drawer.position, Closed.position, Time.deltaTime * 5);
            yield return null;
        }

        Empty();
        yield return new WaitForSeconds(0.5f);

        while (Drawer.position != Opened.position)
        {
            Drawer.position = Vector3.MoveTowards(Drawer.position, Opened.position, Time.deltaTime * 5);
            yield return null;
        }
    }

    IEnumerator _Victory()
    {
        Victory = true;
        yield return new WaitForSeconds(3f);
        VictoryLabel.Enable();
    }

    //Generates a new price, and rounds it to 2 decimals
    IEnumerator _GeneratePrice()
    {
        double rnd = UnityEngine.Random.Range(1.0f, 15f);
        Price = Math.Round(rnd / 50.0, 2) * 50;
        yield return new WaitForSeconds(2);
        OnPriceChange(Price);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Coin c = collision.transform.parent.GetComponent<Coin>();
        if (c != null)
        {
            c.transform.SetParent(c.RegisterTarget, true);
            Compare();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Compare();
    }
}