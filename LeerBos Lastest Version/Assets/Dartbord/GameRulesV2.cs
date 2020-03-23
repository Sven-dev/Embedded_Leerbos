using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameRulesV2 : MonoBehaviour
{
    public GameObject Number;
    List<int> Numbers = new List<int>() { 0, 1, 2, 3, 4 };
    public GameObject Sum;
    public List<int> Cijfers = new List<int>();
    public List<int> CijfersMin = new List<int>();
    public GameObject[] Fields;
    public BoardField[] NumberAdd;
    public int Score = 0;
    public SoundDart AudioManager;
    public DoorBehaviour DoorBehaviour;
    int Thrown = 0;
    public MinPlusScript MinPlusScript;
    public string sum;
    public int Cijvertje;
    public int TotaalScoreInt;
    public string TotaalScore;
    // Use this for initialization
    void Start()
    {

        for (int i = 0; i < Numbers.Count; i++)
        {
            Numbers[i] = Random.Range(12, 30);
        }
        Number.GetComponent<Text>().text =  Numbers[Score].ToString();
        Sum.GetComponent<Text>().text = "...";
    }

    string plusOrMin1 = "";
    string plusOrMin2 = "";

    // Update is called once per frame
    public void SumCount(int LastItemThrown)
    {
        string LastTotalThrown = Thrown.ToString();
        string Oldsum = sum;
        sum = MinPlusScript.SumString;

        StartCoroutine(Fieldsoff());


        Thrown = Cijfers.Sum();
        
        Debug.Log(Thrown);

        Sum.GetComponent<Text>().text = Thrown.ToString();
        int Minus = CijfersMin.Sum();
        Thrown = Thrown - Minus;
        Debug.Log(CijfersMin.Sum());
        Sum.GetComponent<Text>().text = LastTotalThrown + sum + LastItemThrown + " = " + Thrown.ToString();
        if (Thrown.ToString() == Numbers[Score].ToString())
        {
            StartCoroutine(WaitAbit());
        }
        else if(Thrown < Numbers[Score])
        {
            MinPlusScript.Min = true;
            MinPlusScript.Change();
        }
        else if (Thrown > Numbers[Score])
        {
            MinPlusScript.Min = false;
            MinPlusScript.Change();
        }
    }

    public IEnumerator WaitAbit()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(CheckAnswer());
    }

    public IEnumerator CheckAnswer()
    {
        int TotaalScoreInt = Thrown;
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
            else
            {
                Number.GetComponent<Text>().text = "Gooi in totaal " + Numbers[Score].ToString();
                Cijfers.Clear();
                CijfersMin.Clear();
                MinPlusScript.Min = true;
                MinPlusScript.Change();
                Sum.GetComponent<Text>().text = "0";
            }
            Thrown = 0;
            yield return null;
        }
        else
        {
            AudioManager.FalseSound();
            Sum.GetComponent<Text>().text = TotaalScore + " is fout!";
            yield return new WaitForSeconds(1);
            Cijfers.Clear();
            Sum.GetComponent<Text>().text = "0";
            yield return null;
        }
    }
    public void empty()
    {
        Cijfers.Clear();
        Sum.GetComponent<Text>().text = "0";
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
