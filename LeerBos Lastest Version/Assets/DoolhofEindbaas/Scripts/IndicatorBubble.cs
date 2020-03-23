using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorBubble : MonoBehaviour {
    public int WaitTime;
    GameObject[] AllBubbles;

    // Use this for initialization
    void Start() {
        WaitTime = 7;
        StartCoroutine(StartIndicator());
    }

    // Update is called once per frame
    void Update() {
        AllBubbles = GameObject.FindGameObjectsWithTag("IndicatorBubble");
        if (WaitTime < 1 && AllBubbles.Length > 0)
        {
            foreach(GameObject Indi in AllBubbles)
            {
                Indi.GetComponent<BubbleIndicator>().Show();
            }
        }
        else if(WaitTime > 0 && AllBubbles.Length > 0)
        {
            foreach (GameObject Indi in AllBubbles)
            {
                Indi.GetComponent<BubbleIndicator>().Hide();
            }
        }
    }
    IEnumerator StartIndicator()
    {
        for (int i = 0; i < 180; i++)
        {
            WaitTime--;
            yield return new WaitForSeconds(1);
        }
    }
}
