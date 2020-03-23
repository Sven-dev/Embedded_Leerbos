using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAnimatiesDoolhof : MonoBehaviour {
    private float xAs;
    private float yAs;
    private int linksofrechts;
    private float galinks;
    private float garechts;
    public float grote;
    public Sprite pop;
    public int Number = 0;
    private GameObject ThisItem;
    // Use this for initialization
    void Start() {
        grote = Random.Range(1.5f, 2.2f);
        transform.localScale = new Vector2(grote, grote);
        xAs = Random.Range(0.01f,0.01f);
        yAs = Random.Range(0.004f, 0.01f);
        linksofrechts = Random.Range(1, 2);
        galinks = Random.Range(-0, 0.01f);
        garechts = Random.Range(-0.01f, 0);
        ThisItem = this.gameObject;
        StartCoroutine(DestroyObject());
        if (ThisItem.name == "SumBubbles(Clone)")
        {
            ThisItem.tag = "Bubble";
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(xAs, yAs, 0);
        Number++;
        if (linksofrechts == 1)
        {
            xAs = +0.005f;
            if (xAs >= galinks)
            {
                linksofrechts = 2;
            }
        }
        if (linksofrechts == 2)
        {
            xAs = -0.005f;
            if (xAs <= garechts)
            {
                linksofrechts = 1;
            }
        }
    }
    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(Random.Range(15f, 20f));
        if (ThisItem.name == "SumBubbles(Clone)" && ThisItem.tag == "Bubble")
        {
            GetComponent<Animator>().SetBool("Burst",true);
        }
        if (ThisItem.GetComponent<Animator>().GetNextAnimatorStateInfo(0).IsName("Busted"))
        {
            Destroy(ThisItem);
        }
    }
}
