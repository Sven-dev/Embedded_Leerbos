using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Restart : MonoBehaviour, I_SmartwallInteractable
{
    [SerializeField] GameRules controller;
    [SerializeField] private Sprite PushButtonIn;
    [SerializeField] private Sprite PushButtonOut;
    [SerializeField] private GameObject Knop;
    // Use this for initialization
    public void Hit(Vector3 clickposition)
    {
        controller.empty();
        StartCoroutine(PushIn());
    }

    // Update is called once per frame
    void Update () {
		
	}
    IEnumerator PushIn()
    {
        transform.Translate(0.03f,-0.03f,0);
        Knop.GetComponent<SpriteRenderer>().sprite = PushButtonIn;
        yield return new WaitForSeconds(0.2f);
        Knop.GetComponent<SpriteRenderer>().sprite = PushButtonOut;
        transform.Translate(-0.03f, +0.03f, 0);
    }
}
