using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandScript : MonoBehaviour {

    [HideInInspector]
    public bool Flash;
    public Sprite HandOpen;
    public Sprite HandClosed;

    private ArmScript arm;
    private Image hand;
    
	void Start () {
        //get the stuff you need
        arm = transform.parent.GetComponent<ArmScript>();
        hand = GetComponent<Image>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //make sure it's the right trigger
        if (collision.gameObject.tag == "HandTarget")
        {
            //player failed, do the things
            arm.GrabPie();
        }
    }

    public void StartFlashing()
    {
        //hand has entered "danger zone", give visual indicator
        Flash = true;
        StartCoroutine(_FlashHand());
    }

    public void StopFlashing()
    {
        //stops while loop inside coroutine
        Flash = false;
    }

    public IEnumerator _FlashHand()
    {
        //alternate between white and pastel red as long as flash is true
        while(Flash)
        {
            hand.color = Color.Lerp(Color.red, Color.white, 0.3f);
            if (!Flash)
            {
                break;
            }
            yield return new WaitForSeconds(0.25f);
            hand.color = Color.white;
            if (!Flash)
            {
                break;
            }
            yield return new WaitForSeconds(0.25f);
        }
        //make sure it ends on white
        hand.color = Color.white;
    }

    public void OpenHand()
    {
        hand.sprite = HandOpen;
    }

    public void CloseHand()
    {
        hand.sprite = HandClosed;
    }
}
