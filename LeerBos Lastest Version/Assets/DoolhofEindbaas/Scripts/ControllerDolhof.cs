using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerDolhof : MonoBehaviour {

    //Objects for the sum
    public int SumList;
    public List<string> Sum = new List<string>();
    public Text SumText;
    public VragenLijst[] vragen1;
    public VragenLijst[] vragen2;
    public VragenLijst[] vragen3;
    public VragenLijst[] AlleVragen;



    public VragenLijst vraag;

    public bool SumOrNumber;

    //HitpointsBoss
    public GameObject BossHitbar;
    public CatDefeated cat;
    public float HP;
    private float pointsLost;
    public string[] Sums;
    public string[] Numbers;
    public bool CatDizzy;
    public GameObject[] Bubbles;
    public BubblerendererDoolhof Character;
    public ProgressAnimation ProgressAmin;
    public int Sign = 10;


    // Use this for initialization
    public void Install (int Choice) {
        switch (Choice)
        {
            case 0:
                AlleVragen = vragen1;
                break;
            case 1:
                AlleVragen = vragen2;
                break;
            case 2:
                AlleVragen = vragen3;
                break;
        }
        StartCoroutine(Character.BubbleAnimatie());
        vraag = AlleVragen[Random.Range(0, AlleVragen.Length)];
        SumList = 0;
        HP = 0;
        if (SceneManager.GetActiveScene().name != "DolhoofEindbaas")
        {
            SumText.text = vraag.Vraag;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (HP >= 100)
        {
            SumText.text = "Je hebt gewonnen";
        }
        Bubbles = GameObject.FindGameObjectsWithTag("Bubble");
    }
    public void AddSum(string Item)
    {
        if (SceneManager.GetActiveScene().name == "DolhoofEindbaas")
        { 
            Sum[SumList] = Item;
            //SumText.text = Sum[0] + Sum[1] + Sum[2];
            SumList++;
            if (SumList == 3)
            {
                StartCoroutine(Attack());
            }
        }
        else
        {
            Sum[0] = Item;
            if (Sum[0] == vraag.Antwoord)
            {
                SumList = 3;
                
            }
        }
    }
    public IEnumerator Attack()
    {

        //SumText.text = Sum[0] + Sum[1] + Sum[2] + " = " + _dmg.ToString();
        CatDizzy = true;
        SumText.text = vraag.Antwoord;
        bursting();
        while(cat.CatAttacked2 == false)
        {
            yield return null;
        }
        //CatDizzy = false;
        SumList = 0;
        vraag = AlleVragen[Random.Range(0, AlleVragen.Length)];
        SumText.text = vraag.Vraag;
    }



    public IEnumerator CatIsDizzy()
    {
        CatDizzy = true;
        while (cat.CatAttacked2 == false)
        {
            yield return null;
        }
        CatDizzy = false;
    }

    public IEnumerator HitbarLoss()
    {
        var CurrentHPBar = 0.01 * HP;
        Debug.Log(CurrentHPBar);
        if (HP > 100)
        {
            CurrentHPBar = 100;
        }
        Debug.Log(CurrentHPBar);
        StartCoroutine(ProgressAmin.FallingStar());
        while (BossHitbar.transform.localScale.x < CurrentHPBar)
        {
            BossHitbar.transform.localScale += new Vector3(0.005f, 0,0);
            yield return null;
        }

    }
    public void bursting()
    {
        foreach (GameObject target in Bubbles)
        {
            target.GetComponent<Animator>().SetBool("Burst", true);
        }
    }
}
