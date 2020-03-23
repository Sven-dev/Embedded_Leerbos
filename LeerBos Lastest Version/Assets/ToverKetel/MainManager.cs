using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
public class MainManager : MonoBehaviour{


    public MedeklinkerRenderer LijstMedeklinkers;
    // Lijst met alle woorden.
    public static List<WoordenLijst> nietBeAntwoordenWoorden;
    public WoordenLijst[] _WoordenLijst;
    // Plaatjes waar je met de bal kan gooien
    public KlinkerScript Antwoord;
    public KlinkerScript AnderAntwoorden;
    // String met woord dat in het spel wordt gebruikt.
    public WoordenLijst Woord;
    // Lijst met alle letters van een woord.
    private List<string> Letters = new List<string>();
    // ProgressBar dat stijgt bij elk goed antwoord
    public GameObject bar;

    // String voor goede Medeklinker
    private string GoedeMedeklinker;

    // GameObject waar word komt te staan
    [SerializeField] public GameObject FullWord;

    // Scoren die wordt opgeslagen voor refrentie
    [SerializeField] private GameObject scoreref;
    [SerializeField] private GameObject Wintext;
    [SerializeField] public GameObject Slot;
    [SerializeField] public AudioSource Click;
    [SerializeField] public AudioSource False;
    // Use this for initialization
    private void Awake()
    {
        // Pakt score van dont destroy on load.
        if (scoreref == null)
        {
            scoreref = GameObject.Find("WhatDoesNotDestroyOnLoad");
            bar.transform.localScale = new Vector3(scoreref.GetComponent<Instantie>().Scale, 1f, -2);
        }
        if (scoreref.GetComponent<Instantie>().Score >= 1)
        {
            Fade.active = false;
        }
    }

    void Start()
    {
        // Pakt het woordenlijst
        if (nietBeAntwoordenWoorden == null || nietBeAntwoordenWoorden.Count == 0)
        {
            nietBeAntwoordenWoorden = _WoordenLijst.ToList<WoordenLijst>();
        }

        KiesWoord();// Pakt een woord uit het woordenlijst
        if (Woord.Antwoorden.Length == 1)
        { 
            Debug.Log(Woord.Vraag + " Heeft " + Woord.Antwoorden[0]);
        }
        else
        {
            Debug.Log(Woord.Vraag + " Heeft " + Woord.Antwoorden[0] + " en " + Woord.Antwoorden[1]);
        }
        // Zet klinkers van het woord door het scherm.
        LijstMedeklinkers.PuntjesOptellen();
        LijstMedeklinkers.RenderGoedeKlinker();
        // Zet extra klinkers door het scerm heen.
        LijstMedeklinkers.ZetKlinkers();
    }

    // Als op een letter is gegooit dan word er gekeken of het woord bij het plaatje past.
    public KlinkerScript GedrukteKlinker;
    public void CheckOfKlinkerPast(KlinkerScript Klinker)
    {
        GedrukteKlinker = Klinker;
        string lengteklinker = GedrukteKlinker.GetComponent<TextMesh>().text;
        JuisteAntwoord();
        if (GedrukteKlinker.GetComponent<TextMesh>().color != Color.green)
        {
            False.Play();
            GedrukteKlinker.StartCoroutine(GedrukteKlinker.FoutEen());
            GedrukteKlinker.StartCoroutine(GedrukteKlinker.FoutTwee());
        }
    }

    //Alle code als het juist is
    public void JuisteAntwoord()
    {
        for (int i = 0; i < Woord.Antwoorden.Count(); i++)
        {
            if (GedrukteKlinker.KlinkerText == Woord.Antwoorden[i])
            {
                // Zo Ja dan zweeft de letter naar een scherm met lege puntjes.
                Click.Play();
                string Vollewoord = GedrukteKlinker.GetComponent<TextMesh>().text;
                GedrukteKlinker.GetComponent<TextMesh>().color = Color.green;
                int WaarKlinkerInMoet = 0;
                string NieuweWoord = Woord.Vraag;
                var regex = new Regex(Regex.Escape("_"));
                for (int j = 0; j < NieuweWoord.Length; j++)
                {
                    if (NieuweWoord[j] == '_')
                    {
                        NieuweWoord = regex.Replace(NieuweWoord, Vollewoord[WaarKlinkerInMoet].ToString(), 1);
                        WaarKlinkerInMoet += 1;
                    }
                }
                FullWord.GetComponent<TextMesh>().text = NieuweWoord;
                scoreref.GetComponent<Instantie>().Score++;
                StartCoroutine(ProgressBar());
                if (scoreref.GetComponent<Instantie>().Score == 5)
                {
                    Slot.GetComponent<Rigidbody2D>().gravityScale = 1;
                    Wintext.GetComponent<RectTransform>().position = new Vector3(0, 0, -3);
                    StartCoroutine(OpenDeuren());
                }
                else
                {
                    if (Wintext.GetComponent<RectTransform>().position.z != -3 )
                    {
                        StartCoroutine(volgendeWoord());
                    }
                }
            }
        }

    }
    //Score dat ervoor zorgt dat als je goed hebt gegooid het spel op pauze zet.
    IEnumerator volgendeWoord()
    {
        nietBeAntwoordenWoorden.Remove(Woord);

        yield return new WaitForSeconds(2.5f);
        if (LijstMedeklinkers.sceneName == "Toverketel")
        {
            SceneManager.LoadScene("Toverketel");
        }
        else if (LijstMedeklinkers.sceneName == "memory")
        {
            SceneManager.LoadScene("memory");
        }
    }


    public GameObject deurlinks;
    public GameObject deurrechts;
    IEnumerator OpenDeuren()
    {
        for (int i = 0; i < 25; i++)
        {
            deurlinks.transform.Rotate(Vector3.up, 1.5f);
            deurrechts.transform.Rotate(Vector3.down, 1.5f);
            yield return null;
        }
        Victory();
    }
    // Zo nee dan gebeurd er niets.

    // Als de letter naar tevoren is gekomen Dan komt er een victory screen.



    void Update () {
		//(Optioneel) Laat vliegende boeken over het scherm bewegen.
	}

    void KiesWoord()
    {
        int RandomWoord = Random.Range(0, nietBeAntwoordenWoorden.Count);
        Woord = nietBeAntwoordenWoorden[RandomWoord];

        FullWord.GetComponent<TextMesh>().text = Woord.Vraag;
    }


    //Animaties van de progressbar.
    IEnumerator ProgressBar()
    {
        if (scoreref.GetComponent<Instantie>().Score == 1)
        {
            while(bar.transform.localScale.x <= 1)
            {
                bar.transform.localScale += new Vector3(0.1f, 0, 0);
                scoreref.GetComponent<Instantie>().Scale = bar.transform.localScale.x;
                yield return null;
            }       
        }
        if (scoreref.GetComponent<Instantie>().Score == 2)
        {
            while (bar.transform.localScale.x <= 2.27)
            {
                bar.transform.localScale += new Vector3(0.1f, 0, 0);
                scoreref.GetComponent<Instantie>().Scale = bar.transform.localScale.x;
                yield return null;
            }
        }
        if (scoreref.GetComponent<Instantie>().Score == 3)
        {
            while (bar.transform.localScale.x <= 3.6)
            {
                bar.transform.localScale += new Vector3(0.1f, 0, 0);
                scoreref.GetComponent<Instantie>().Scale = bar.transform.localScale.x;
                yield return null;
            }
        }
        if (scoreref.GetComponent<Instantie>().Score == 4)
        {
            while (bar.transform.localScale.x <= 5.0)
            {
                bar.transform.localScale += new Vector3(0.1f, 0, 0);
                scoreref.GetComponent<Instantie>().Scale = bar.transform.localScale.x;
                yield return null;
            }
        }
        if (scoreref.GetComponent<Instantie>().Score == 5)
        {
            while (bar.transform.localScale.x <= 6.11)
            {
                bar.transform.localScale += new Vector3(0.1f, 0, 0);
                scoreref.GetComponent<Instantie>().Scale = bar.transform.localScale.x;
                yield return null;
            }
        }
    }
    public VictoryScript VictoryCanvas;
    private void Victory()
    {
        StartCoroutine(_Victory());
    }

    IEnumerator _Victory()
    {
        yield return new WaitForSeconds(0.5f);
        VictoryCanvas.Enable();
    }
    public GameObject Fade;

}
