using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CatDizzy : MonoBehaviour, I_SmartwallInteractable {
    [SerializeField] private ControllerDolhof Controlller;
    public GameObject Indicator;
    public GameObject SecondIndicator;
    public GameObject Cat;
    public Material CatHurt;
    public Material CatIdle;
    public GameObject[] FacialFeatures;
    public AudioSource hit;
	// Use this for initialization
	void Start () {
	
	}

    private void Update()
    {
        if (Controlller.CatDizzy == true)
        {
            Indicator.GetComponent<TutorialIndicator>().Active = true;
            Indicator.GetComponent<TutorialIndicator>().Show();
            SecondIndicator.GetComponent<TutorialIndicator>().Active = true;
            SecondIndicator.GetComponent<TutorialIndicator>().Show();
        }
        else if(Controlller.CatDizzy == false)
        {
            Indicator.GetComponent<TutorialIndicator>().Active = false;
            Indicator.GetComponent<TutorialIndicator>().Hide();
            SecondIndicator.GetComponent<TutorialIndicator>().Active = false;
            SecondIndicator.GetComponent<TutorialIndicator>().Hide();
        }
    }

    // Update is called once per frame
    public void Hit(Vector3 clickposition)
    {
        Debug.Log(Controlller.CatDizzy);
        if (Controlller.CatDizzy == true)
        {
            Controlller.HP += 20f;
            StartCoroutine(CatHit());
            StartCoroutine(Controlller.HitbarLoss());
            //StartCoroutine(CatHit());
        }
	}
    public IEnumerator CatHit()
    {
        Debug.Log("Playing");
        hit.Play();
        Cat.GetComponent<Renderer>().material = CatHurt;
        foreach(GameObject Eyes in FacialFeatures)
        {
            Eyes.SetActive(false);
        }
        if (Cat.GetComponent<CatDefeated>().CatAttacked2 == false)
        {
            Cat.GetComponent<CatDefeated>().CatAttacked2 = true;
        }
        else
        {
            Cat.GetComponent<CatDefeated>().CatAttacked2 = false;
        }
        yield return new WaitForSeconds(0.3f);
        Cat.GetComponent<Renderer>().material = CatIdle;
        foreach (GameObject Eyes in FacialFeatures)
        {
            Eyes.SetActive(true);
        }
    }
}
