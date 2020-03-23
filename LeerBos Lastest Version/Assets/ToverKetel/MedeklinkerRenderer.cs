using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MedeklinkerRenderer : MonoBehaviour {
    [SerializeField] public MainManager Controller;
    public GameObject AnderAntwoord;//Andere Antwoorden
    public GameObject Antwoord;//Juiste Antwoord
    public List<Sprite> Toverdrank;

    //Lijsten met alle klinkers
    public List<string> EenKlinker = new List<string>();
    public List<string> TweeKlinkers = new List<string>();
    public List<string> DrieKlinkers = new List<string>();
    private List<string> GebruikteKlinkers = new List<string>();//Alle klinkers
    public List<Vector3> Posities = new List<Vector3>();
    public string sceneName;

    public void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
         sceneName = currentScene.name;
    }
    //Kijkt hoe veel klinkers het woord heeft zodat het spel weet welke klinkers hij kan pakken.
    public void PuntjesOptellen()
    {
        if(Controller.Woord.Antwoorden[0].Length == 1)
        {
            GebruikteKlinkers = EenKlinker;
        }
        else if (Controller.Woord.Antwoorden[0].Length == 2)
        {
            GebruikteKlinkers = TweeKlinkers;
        }
        else if (Controller.Woord.Antwoorden[0].Length == 3)
        {
            GebruikteKlinkers = DrieKlinkers;
        }
    }

    // Verdeeld klinker die bij het woord past op het scherm
    public void RenderGoedeKlinker()
    {
        if (sceneName == "Toverketel")
        {
            Antwoord.GetComponent<TextMesh>().text = Controller.Woord.Antwoorden[Random.Range(0, Controller.Woord.Antwoorden.Count())];
            Antwoord.transform.position = Posities[Random.Range(0, Posities.Count)];
            Antwoord.GetComponentInChildren<SpriteRenderer>().sprite = Toverdrank[Random.Range(0, Toverdrank.Count)];
            for (int i = 0; i < GebruikteKlinkers.Count; i++)
            {
                Debug.Log("fwf");
                if (GebruikteKlinkers[i] == Antwoord.GetComponent<TextMesh>().text)
                {
                    GebruikteKlinkers.Remove(GebruikteKlinkers[i]);
                    Posities.Remove(Antwoord.transform.position);
                    Toverdrank.Remove(Antwoord.GetComponentInChildren<SpriteRenderer>().sprite);
                    Debug.Log("werkt");
                }
            }
        }
        if (sceneName == "ToverketelAdvanced")
        {
            Antwoord.GetComponent<TextMesh>().text = Controller.Woord.Antwoorden[Random.Range(0, Controller.Woord.Antwoorden.Count())];
            Antwoord.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(-4.5f, 4.5f), -0.3f);
            Antwoord.GetComponentInChildren<SpriteRenderer>().sprite = Toverdrank[Random.Range(0, Toverdrank.Count)];
            Toverdrank.Remove(Antwoord.GetComponentInChildren<SpriteRenderer>().sprite);
            for (int i = 0; i < GebruikteKlinkers.Count; i++)
            {
                Debug.Log("fwf");
                if (GebruikteKlinkers[i] == Antwoord.GetComponent<TextMesh>().text)
                {
                    GebruikteKlinkers.Remove(GebruikteKlinkers[i]);
                    Debug.Log("werkt");
                }
            }
        }
    }

    // Verdeeld klinkers over de hele scherm.
    // Twee opties Mogelijk
    // 1. Random over het hele scherm.(Nu gecodeerd)
    // 2. Op Bepaalde plekken op hett scherm zoals de tafel of op planken.
    public void ZetKlinkers()
    {
        if (sceneName == "Toverketel")
        {
            AnderAntwoord.GetComponent<TextMesh>().text = GebruikteKlinkers[Random.Range(0, GebruikteKlinkers.Count)];
            GebruikteKlinkers.Remove(AnderAntwoord.GetComponent<TextMesh>().text);
            AnderAntwoord.transform.position = Posities[Random.Range(0, Posities.Count)];
            Posities.Remove(AnderAntwoord.transform.position);
            AnderAntwoord.GetComponentInChildren<SpriteRenderer>().sprite = Toverdrank[Random.Range(0, Toverdrank.Count)];
            Toverdrank.Remove(AnderAntwoord.GetComponentInChildren<SpriteRenderer>().sprite);

            for (int i = 0; i < 3; i++)
            {
                var position = Posities[Random.Range(0, Posities.Count)];
                Instantiate(AnderAntwoord, position, transform.rotation);
                Posities.Remove(position);
                AnderAntwoord.GetComponent<TextMesh>().text = GebruikteKlinkers[Random.Range(0, GebruikteKlinkers.Count)];
                GebruikteKlinkers.Remove(AnderAntwoord.GetComponent<TextMesh>().text);
                AnderAntwoord.GetComponentInChildren<SpriteRenderer>().sprite = Toverdrank[Random.Range(0, Toverdrank.Count)];
                Toverdrank.Remove(AnderAntwoord.GetComponentInChildren<SpriteRenderer>().sprite);
            }
        }
        else if (sceneName == "ToverketelAdvanced")
        {
            AnderAntwoord.GetComponent<TextMesh>().text = GebruikteKlinkers[Random.Range(0, GebruikteKlinkers.Count)];
            GebruikteKlinkers.Remove(AnderAntwoord.GetComponent<TextMesh>().text);

            AnderAntwoord.GetComponentInChildren<SpriteRenderer>().sprite = Toverdrank[Random.Range(0, Toverdrank.Count)];
            Toverdrank.Remove(AnderAntwoord.GetComponentInChildren<SpriteRenderer>().sprite);
            for (int i = 0; i < 3; i++)
            {
                var position = new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(-4.5f, 4.5f), -0.3f);
                Instantiate(AnderAntwoord, position, transform.rotation);

                AnderAntwoord.GetComponent<TextMesh>().text = GebruikteKlinkers[Random.Range(0, GebruikteKlinkers.Count)];
                AnderAntwoord.GetComponentInChildren<SpriteRenderer>().sprite = Toverdrank[Random.Range(0, Toverdrank.Count)];
                Toverdrank.Remove(AnderAntwoord.GetComponentInChildren<SpriteRenderer>().sprite);
                GebruikteKlinkers.Remove(AnderAntwoord.GetComponent<TextMesh>().text);
            }
        }
    }
}
// (Optioneel) Zet random letters op kasten en vliegende boeken.

