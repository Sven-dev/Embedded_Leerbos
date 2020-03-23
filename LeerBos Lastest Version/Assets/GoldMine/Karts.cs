using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Karts : MonoBehaviour {
    [SerializeField] private GameObject Kart;
    [SerializeField] private GameObject Link;
    [SerializeField] private GameObject KartBack;
    [SerializeField] public Sentences[] Sentences;
    [SerializeField] public GameObject Train;
    [SerializeField] private GameObject TrainBack;
    [SerializeField] private Bands BandHole;

    [SerializeField] private Transform targetBegin;
    [SerializeField] public Transform targetMiddle;
    [SerializeField] private Transform targetEnd;
    [SerializeField] private Gold Gold;
    public int VictoryPoints = 0;
    [SerializeField] AudioSource Grind;
    [SerializeField] private GameObject VictoryScreen;
    private int EmptyWord;

    public Sentences Sentence;
    // Use this for initialization
    void Awake() {
        SpawnKarts();
    }
    private void Update()
    {

    }
    // Update is called once per frame
    void SpawnKarts() {
        Train.transform.position = targetBegin.transform.position;
        Sentence = Sentences[Random.Range(0, Sentences.Length)];
        EmptyWord = Emptyfy();
        for (int i = 0; i < Sentence.Words.Length; i++)
        {
            InstantiateObjects(KartBack, i);
        }
        for (int i = 0; i < Sentence.Words.Length; i++)
        {
            InstantiateObjects(Kart, i);
            if (i != Sentence.Words.Length - 1)
            {
                InstantiateObjects(Link, i);
            }
        }
        BandHole.CountTrains();
        StartCoroutine(TrainEnters());
        //StartCoroutine(TrainEnters(TrainBack));
    }

    void InstantiateObjects(GameObject Item, int i)
    {
        GameObject NewItem;
        int PosX = 250 * i;
        Vector2 Positie = new Vector2(Kart.GetComponent<RectTransform>().localPosition.x + PosX, Kart.GetComponent<RectTransform>().transform.localPosition.y);
        NewItem = Instantiate(Item, Positie, Kart.GetComponent<RectTransform>().transform.rotation);
        if (NewItem.GetComponentInChildren<Text>() != null)
        {
            NewItem.GetComponentInChildren<KartScript>().Word = Sentence.Words[i];

            if (EmptyWord == i)
            {
                NewItem.GetComponentInChildren<Text>().text = "...";
                Sentence.Words[i].Disappear = true;
            }
            else
            {
                NewItem.GetComponentInChildren<Text>().text = Sentence.Words[i].Word;
            }
        }

        NewItem.transform.SetParent(GameObject.FindGameObjectWithTag("Kart").transform, false);
    }

     int Emptyfy()
    {
        int EmptyWord1 = Random.Range(0, Sentence.Words.Length);

        int[] EmptyWords = { EmptyWord1};
        return EmptyWord1;
    }

    IEnumerator TrainEnters()
    {
        while (Vector2.Distance(Train.transform.position, targetMiddle.position) >= 0.01f)
        {
            Train.transform.position = Vector2.Lerp(Train.transform.position, targetMiddle.position, Time.deltaTime * 2f);
            // print("train: "  + Train.transform.position);
            Grind.volume = 0.5f * Vector2.Distance(Train.transform.position, targetMiddle.position);
            yield return null;
        }
    }
    IEnumerator TrainLeaves()
    {
        while (Vector2.Distance(Train.transform.position, targetEnd.position) >= 1f)
        {
            Train.transform.position = Vector2.Lerp(Train.transform.position, targetEnd.position, Time.deltaTime * 2f);
            // print("train: "  + Train.transform.position);
            Grind.volume = 0.5f * Vector2.Distance(Train.transform.position, targetMiddle.position);
            yield return null;
        }
        GameObject[] AllLinks = GameObject.FindGameObjectsWithTag("LinkPiece");
        GameObject[] BackKart = GameObject.FindGameObjectsWithTag("KartBack");
        GameObject[] AllKarts = GameObject.FindGameObjectsWithTag("TrainPiece");
        for (int i = 1; i < AllKarts.Length; i++)
        {
            //if (AllLinks.Length > 1)
            //{
            //    Destroy(AllLinks[i]);
            //}
            Destroy(BackKart[i]);
            GameObject.Destroy(AllKarts[i]);
        }
        while (BandHole.Karts.Count > 0)
        {
            BandHole.Karts.Remove(BandHole.Karts[0]);
            Debug.Log(BandHole.Karts.Count);
        }
        BandHole.Karts.Clear();
        for (int i = 1; i < AllLinks.Length; i++)
        {
            Destroy(AllLinks[i]);
        }

        SpawnKarts();
        Gold.Respawn();
    }
    public IEnumerator TrainWait()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(TrainLeaves());
    }
}
