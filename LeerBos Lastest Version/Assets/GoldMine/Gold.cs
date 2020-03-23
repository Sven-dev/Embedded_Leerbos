using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Gold : MonoBehaviour {
    [SerializeField] private GameObject GoldBlock;
    [SerializeField] private Karts SentenceMissingParts;
    [SerializeField] private Sprite[] VisualBlock;
    private List<string> AllWords = new List<string>();
    private List<string> trueWords = new List<string>();
    [SerializeField] private List<float> Positions = new List<float>();
     private List<float> Positions2 = new List<float>();

    public bool Starting = true;
    // Use this for initialization
    void Start () {
        Spawn();
    }
	
	// Update is called once per frame
	public void Respawn()
    {
	    foreach(GameObject GoldBlock in GameObject.FindGameObjectsWithTag("Gold"))
        {
            if (GoldBlock.name == "Gold(Clone)")
            {
                Destroy(GoldBlock);
            }
        }
        Starting = true;
        Spawn();
	}

    void Spawn()
    {
        Positions2.Clear();
        for(int i = 0; i < Positions.Count; i++)
        {
            Positions2.Add(Positions[i]);
        }
        for (int i = 0; i < SentenceMissingParts.Sentence.Words.Length; i++)
        {
            AllWords.Add(SentenceMissingParts.Sentence.Words[i].Word);
            if (SentenceMissingParts.Sentence.Words[i].Disappear == true)
            {
                trueWords.Add(SentenceMissingParts.Sentence.Words[i].Word);
            }
        }
        for (int i = 0; i < SentenceMissingParts.Sentence.falseWords.Length; i++)
        {
            AllWords.Add(SentenceMissingParts.Sentence.falseWords[i]);
        }
        for (int i = 0; i < 6; i++)
        {
            SpawnGold(i);
        }
        Starting = false;
    }


    public void SpawnGold(int i)
    {
        GameObject NewItem;
        Vector2 Positie = new Vector2(Positions2[Random.Range(0,Positions2.Count)], Random.Range(250,-100));
        Positions2.Remove(Positie.x);
        NewItem = Instantiate(GoldBlock, Positie, GoldBlock.transform.rotation);



        NewItem.GetComponent<Image>().sprite = VisualBlock[Random.Range(0, VisualBlock.Length)];
        if ((trueWords == null) || (!trueWords.Any()))
        {
            if (Starting == true)
            { 
                NewItem.GetComponentInChildren<Text>().text = SentenceMissingParts.Sentence.falseWords[Random.Range(0, SentenceMissingParts.Sentence.falseWords.Length)];
            }
            else if(Starting == false)
            {
                NewItem.GetComponentInChildren<Text>().text = SentenceMissingParts.Sentence.Words[Random.Range(0, SentenceMissingParts.Sentence.Words.Length)].Justwords[0];
            }
        }
        else
        {
            NewItem.GetComponentInChildren<Text>().text = trueWords[0];
            trueWords.Remove(NewItem.GetComponentInChildren<Text>().text);
        }
        NewItem.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }
}
