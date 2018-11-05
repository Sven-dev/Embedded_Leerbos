using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public int Rounds;
    public double Price;

    public Text PriceLabel;
    public Transform CorrectTarget;
    private bool Comparing;

    public delegate void PriceChange();
    public event PriceChange OnPriceChange;
    public UIPrice UILabel;
    public VictoryScript VictoryLabel;
    private bool Victory;

    private AudioSource Audio;


    private void Start()
    {
        UILabel.Link(this);
        Comparing = false;
        Victory = false;
        Audio = GetComponent<AudioSource>();
        StartCoroutine(_GeneratePrice());
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
        Coin[] correctcoins = transform.GetComponentsInChildren<Coin>();
        foreach (Coin c in correctcoins)
        {
            Destroy(c.transform.GetChild(0).GetComponent<Collider2D>());
            c.CorrectCoin = true;
            c.MoveTo(CorrectTarget, false);
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
        double rnd = UnityEngine.Random.Range(2.0f, 20f);
        Price = Math.Round(rnd / 5.0, 2) * 5;
        yield return new WaitForSeconds(2);
        OnPriceChange();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Coin c = collision.transform.parent.GetComponent<Coin>();
        if (c != null)
        {
            c.transform.SetParent(transform, true);
            Compare();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Compare();
    }
}