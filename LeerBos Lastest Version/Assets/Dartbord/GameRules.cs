using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRules : MonoBehaviour {
    public GameObject Number;
    List<int> Numbers = new List<int>() {0,1,2,3,4 };
    public GameObject Sum;
    public List<int> Cijfers = new List<int>();
    public GameObject[] Fields;
    public BoardField[] NumberAdd;
    public int Score = 0;
    public SoundDart AudioManager;
    public DoorBehaviour DoorBehaviour;

    public MinPlusScript MinPlusScript;

    public int TotaalScoreInt;
    public string TotaalScore;
    // Use this for initialization
    void Start () {

		for (int i = 0; i< Numbers.Count; i++)
        {
            Numbers[i] = Random.Range(12, 30);
        }
        Number.GetComponent<Text>().text = "Gooi in totaal " + Numbers[Score].ToString();
        Sum.GetComponent<Text>().text = ".. + .. + .. =..";
    }

    string plusOrMin1 = "";
    string plusOrMin2 = "";

    // Update is called once per frame
    public void SumCount() {

        string sum = MinPlusScript.SumString;

        StartCoroutine(Fieldsoff());

        if (MinPlusScript.Min == false && Cijfers.Count == 2)
        {
            plusOrMin1 = " + ";
        }
        if (MinPlusScript.Min == false && Cijfers.Count == 3)
        {
            plusOrMin2 = " + ";
        }

        if (Cijfers.Count == 0)
        {
            Sum.GetComponent<Text>().text = ".." + sum + ".." + sum  + ".." + "=..";
        }
        if (Cijfers.Count == 1)
        {
            Sum.GetComponent<Text>().text = Cijfers[0].ToString() + sum + ".." + sum + ".." + "=..";
        }
        if (Cijfers.Count == 2)
        {
            Sum.GetComponent<Text>().text = Cijfers[0].ToString() + plusOrMin1 + Cijfers[1].ToString() + sum + ".." + "=..";
        }
        if (Cijfers.Count == 3)
        {
            int TotaalScoreInt = Cijfers[0] + Cijfers[1]+ Cijfers[2];
            Sum.GetComponent<Text>().text = Cijfers[0].ToString() + plusOrMin1 + Cijfers[1].ToString() + plusOrMin2 + Cijfers[2].ToString() + " = " + TotaalScoreInt.ToString();
            StartCoroutine(WaitAbit());
        }
    }

    public IEnumerator WaitAbit()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(CheckAnswer());
    }

    public IEnumerator CheckAnswer()
    {
        int TotaalScoreInt = Cijfers[0] + Cijfers[1] + Cijfers[2];
        Debug.Log(TotaalScoreInt.ToString());
        Debug.Log(Numbers[Score].ToString());
        if (TotaalScoreInt.ToString() == Numbers[Score].ToString())
        {
            AudioManager.CorrectSound();
            Sum.GetComponent<Text>().text = TotaalScore + " is goed!";
            DoorBehaviour.LockBehavoir();
            yield return new WaitForSeconds(1);
            Score++;
            if (Score == 4)
            {
                Sum.GetComponent<Text>().text = TotaalScore + " Je Wint!";
                Number.GetComponent<Text>().text = "Geweldig";
            }
            else { 
                Number.GetComponent<Text>().text = "Gooi in totaal " + Numbers[Score].ToString();
                Cijfers.Clear();
                Sum.GetComponent<Text>().text = "..+..+..=..";
            }
            yield return null;
        }
        else
        {
            AudioManager.FalseSound();
            Sum.GetComponent<Text>().text = TotaalScore + " is fout!";
            yield return new WaitForSeconds(1);
            Cijfers.Clear();
            Sum.GetComponent<Text>().text = "..+..+..=..";
            yield return null;
        }
    }
    public void empty()
    {
        Cijfers.Clear();
        Sum.GetComponent<Text>().text = "..+..+..=..";
    }

    //Zorgt dat niet tegegijk meerdere fields worden gebruikt 

    public void EnableFields()
    {
        for (int i = 0; i < Fields.Length; i++)
        {
            Fields[i].GetComponent<Collider2D>().enabled = true;
        }
    }

    public void DisableFields()
    {
        for (int i = 0; i < Fields.Length; i++)
        {
            Fields[i].GetComponent<Collider2D>().enabled = false;
        }
    }
    IEnumerator Fieldsoff()
    {
        DisableFields();
        yield return new WaitForSeconds(0.25f);
        EnableFields();
    }
}
