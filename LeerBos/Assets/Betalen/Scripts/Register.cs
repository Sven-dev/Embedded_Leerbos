using System;
using System.Collections;
using UnityEngine;

public class Register : MonoBehaviour
{
    public int Rounds;
    public VictoryScript VictoryLabel;
    private bool Victory;
    [HideInInspector]
    public double Price;

    [Space]
    [Header("Drawer states")]
    public Transform Closed;
    public Transform Opened;
    private Transform Drawer;
    public Lights Lamps;

    [Space]
    public AudioClip Correct;
    public AudioClip Drag;
    private AudioSource Audio;

    [HideInInspector]
    public bool CorrectAnswer;
    public delegate void PriceChange(double price);
    public static event PriceChange OnPriceChange;

    private void Start()
    {
        CorrectAnswer = false;
        Victory = false;
        Audio = GetComponent<AudioSource>();
        Drawer = transform.GetChild(0);

        StartCoroutine(_GeneratePrice());
    }

    //Compares the required price to the sum of all coins in the counter
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

    //Removes all coins from the register
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
        yield return new WaitForSeconds(1);

        Audio.PlayOneShot(Correct);
        Lamps.Flicker();

        #region Close drawer
        while (Drawer.position != Closed.position)
        {
            Drawer.position = Vector3.MoveTowards(Drawer.position, Closed.position, Time.deltaTime * 6);
            yield return null;
        }
        #endregion

        Empty();
        Rounds--;
        if (Rounds == 0)
        {
            CorrectAnswer = false;
            yield return new WaitForSeconds(0.5f);
            Win();
        }
        else
        {
            #region Generate new price
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(_GeneratePrice());
            yield return new WaitForSeconds(0.8f);
            Lamps.Stop();
            yield return new WaitForSeconds(0.25f);
            #endregion

            CorrectAnswer = false;

            #region Open drawer
            Audio.PlayOneShot(Drag);
            while (Drawer.position != Opened.position)
            {
                Drawer.position = Vector3.MoveTowards(Drawer.position, Opened.position, Time.deltaTime * 6);
                yield return null;
            }
            #endregion
        }
    }

    //Generates a new price, and rounds it to .50 cents
    IEnumerator _GeneratePrice()
    {
        double rnd = UnityEngine.Random.Range(1.5f, 15f);
        rnd = Math.Round(rnd / 50.0, 2) * 50;

        //Ensure the next price is never the same as the last one
        if (rnd == Price)
        {
            StartCoroutine(_GeneratePrice());
        }
        else
        {
            Price = rnd;
            yield return null;
            OnPriceChange(Price);
        }
    }

    private void Win()
    {
        Victory = true;
        VictoryLabel.Enable();
    }
}