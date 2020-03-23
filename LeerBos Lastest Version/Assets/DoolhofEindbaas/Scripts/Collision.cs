using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {
    public GameObject PawCollider;
    public PawScript Paw;
    public AudioSource hit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.name == "Character")
        {
            this.GetComponent<Rigidbody2D>().isKinematic = false;
            Debug.Log("Character got hit");
            StartCoroutine(Paw.CharacterDown());
            StartCoroutine(Paw.CharacterUnable());
            StartCoroutine(Paw._WaitBurst());
            StartCoroutine(Paw.StartBubbles());
            hit.Play();
            Paw.AnimAppleStart = true;
        }
        else if(col.collider.name == "SumBubbles(Clone)")
        {
            col.collider.GetComponent<Animator>().SetBool("Burst", true);
        }
    }
}
