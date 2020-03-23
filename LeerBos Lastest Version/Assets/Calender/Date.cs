using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Date : MonoBehaviour, I_SmartwallInteractable
{
    public string answer;
    [SerializeField] CalenderManager controller;
    [SerializeField] AudioSource audioS;
    [SerializeField] AudioClip AnswerFalse;
    [SerializeField] AudioClip AnswerGood;
    [SerializeField] Sprite GoodSprite;
    [SerializeField] Sprite FalseSprite;
    [SerializeField] Sprite NormalSprite;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Hit(Vector3 hitPosition)
    {
        StartCoroutine(check());
    }
    IEnumerator check()
    {
        yield return new WaitForSeconds(0.2f);

        if (controller.question.answer == answer)
        {
            audioS.clip = AnswerGood;
            audioS.Play();
            gameObject.GetComponent<Image>().sprite = GoodSprite;
            StartCoroutine(VictoryWait());
        }
        else
        {
            StartCoroutine(falseSprite());
            audioS.clip = AnswerFalse;
            audioS.Play();
        }
    }
    IEnumerator VictoryWait()
    {
        controller.text.text = "Goed!!";
        yield return new WaitForSeconds(2);
        controller.score++;
        controller.NewQuestion();
    }
    IEnumerator falseSprite()
    {
        gameObject.GetComponent<Image>().sprite = FalseSprite;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Image>().sprite = NormalSprite;


    }
}