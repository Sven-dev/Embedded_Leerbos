using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAnimaties : MonoBehaviour {
    private float xAs;
    private float yAs;
    private int linksofrechts;
    private float galinks;
    private float garechts;
    public float grote;
    public Sprite pop;
    public int Number = 0;
    // Use this for initialization
    void Start() {
        grote = Random.Range(0.3f, 1.2f);
        transform.localScale = new Vector2(grote, grote);
        xAs = Random.Range(-0.02f,0.02f);
        yAs = Random.Range(0, 0.02f);
        linksofrechts = Random.Range(1, 2);
        galinks = Random.Range(-0, 0.05f);
        garechts = Random.Range(-0.05f, 0);
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
}
