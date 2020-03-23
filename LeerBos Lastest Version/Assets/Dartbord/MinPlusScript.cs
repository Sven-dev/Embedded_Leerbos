using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinPlusScript : MonoBehaviour, I_SmartwallInteractable
{
    public bool Min = false;
    [SerializeField] private SpriteRenderer SpriteObject;
    [SerializeField] private Sprite MinSprite;
    [SerializeField] private Sprite PlusSprite;
    [SerializeField] private Sprite PushButtonIn;
    [SerializeField] private Sprite PushButtonOut;
    [SerializeField] private GameObject Knop;
    [SerializeField] public string SumString;
    [SerializeField] private GameRules Controller;


    // Use this for initialization

    public void Hit(Vector3 clickposition) {
        Change();
        StartCoroutine(PushIn());
        Controller.SumCount();
    }

    // Update is called once per frame
    public void Change() {
        if (Min == false)
        {
            Min = true;
            SpriteObject.sprite = PlusSprite;
            SumString = " - ";
        }
        else if (Min == true)
        {
            Min = false;
            SpriteObject.sprite = MinSprite;
            SumString = " + ";
        }
    }
    IEnumerator PushIn()
    {
        transform.Translate(0.03f, -0.03f, 0);
        Knop.GetComponent<SpriteRenderer>().sprite = PushButtonIn;
        yield return new WaitForSeconds(0.2f);
        Knop.GetComponent<SpriteRenderer>().sprite = PushButtonOut;
        transform.Translate(-0.03f, +0.03f, 0);
    }   
}
