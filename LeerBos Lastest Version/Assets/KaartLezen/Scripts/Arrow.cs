using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, I_SmartwallInteractable {
    [SerializeField] public MapReadingMainManager Controller;
    Vector2 pos;
    [SerializeField] public TutorialIndicator Indicator;
    [SerializeField] public AudioClip GoodAnswer;
    [SerializeField] public AudioClip FalseAnswer;

    // Use this for initialization
    void Start () {
        pos = transform.position;
        if (Indicator != null)
        {
            Indicator.Image.enabled = false;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Hit(Vector3 clickposition)
    {
        if (Controller.wait == true)
        {
            GameObject[] Arrows = GameObject.FindGameObjectsWithTag("Arrow");
            for (int i = 0; i < Arrows.Length; i++)
            {
                Arrows[i].GetComponentInChildren<TutorialIndicator>().Hide();
            }
            pos = transform.position;
            Debug.Log(Vector2.Distance(pos, Controller.pos));
            if (Vector2.Distance(pos, Controller.pos) < 0.8f)
            {
                GetComponent<AudioSource>().clip = GoodAnswer;
                GetComponent<AudioSource>().Play();
                Controller.nextQuestion();
                Controller.wait = false;
            }
            else
            {
                GetComponent<AudioSource>().clip = FalseAnswer;
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
