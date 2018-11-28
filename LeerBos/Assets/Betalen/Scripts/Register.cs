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
    public bool CorrectAnswer;

    public delegate void PriceChange(double price);
    public static event PriceChange OnPriceChange;

    public Lights Lamps;
    public VictoryScript VictoryLabel;
    private bool Victory;
    [Space]
    public AudioClip Correct;
    public AudioClip Drag;
    private AudioSource Audio;

    private void Start()
    {
        CorrectAnswer = false;
        Victory = false;
        Audio = GetComponent<AudioSource>();
        Drawer = transform.GetChild(0);

        StartCoroutine(_Start());
    }

    IEnumerator _Start()
    {
        yield return new WaitForSeconds(1.5f);
        GeneratePrice();
    }

    //Compares the required price to the sum of all coint that are children of the counter
    public void Compare()
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
        CorrectAnswer = true;
        yield return new WaitForSeconds(2);

        Audio.PlayOneShot(Correct);
        Lamps.Flicker();

        //Close drawer
        while (Drawer.position != Closed.position)
        {
            Drawer.position = Vector3.MoveTowards(Drawer.position, Closed.position, Time.deltaTime * 6);
            yield return null;
        }

        Empty();
        Rounds--;
        if (Rounds == 0)
        {
            CorrectAnswer = false;
            yield return new WaitForSeconds(1f);
            Win();
        }
        else
        {
            yield return new WaitForSeconds(0.25f);
            GeneratePrice();
            yield return new WaitForSeconds(0.5f);


            //Open drawer
            Audio.PlayOneShot(Drag);
            while (Drawer.position != Opened.position)
            {
                Drawer.position = Vector3.MoveTowards(Drawer.position, Opened.position, Time.deltaTime * 6);
                yield return null;
            }

            Lamps.Stop();
            CorrectAnswer = false;
        }
    }

    private void Win()
    {
        Victory = true;
        VictoryLabel.Enable();
    }

    //Generates a new price, and rounds it to .50 cents
    private void GeneratePrice()
    {
        double rnd = UnityEngine.Random.Range(1.0f, 15f);
        Price = Math.Round(rnd / 50.0, 2) * 50;
        OnPriceChange(Price);
    }
}