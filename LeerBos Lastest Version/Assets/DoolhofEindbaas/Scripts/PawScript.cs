using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawScript : MonoBehaviour {
    public GameObject Paw;
    private GameObject InstancePaw;
    private Vector3 SpawnPoint;
    public GameObject Character;
    public bool AllowPaw;
    public GameObject[] Bubbles;
    public bool hit;

    public ControllerDolhof HP;

    public AudioSource Whoosh;
    public AudioSource PawInTree;
    public Animator CatPaw;

    public bool AnimAppleStart;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(WaitTime());
    }

    // Update is called once per frame
    void Update() {
        Bubbles = GameObject.FindGameObjectsWithTag("Bubble");
    }
    IEnumerator SpawnPaw()
    {
        PawInTree.Play();
        if (Character.transform.position.x < 0)
        {
            CatPaw.SetInteger("Bool", 1);
        }
        else
        {
            CatPaw.SetInteger("Bool", 2);
        }
        yield return new WaitForSeconds(1);
        SpawnPoint = new Vector3(Character.transform.position.x, Character.transform.position.y + 10, 96);

        InstancePaw = Instantiate(Paw, SpawnPoint, Quaternion.Euler(new Vector3(0f, 96, 0f)));
        Whoosh.Play();
        StartCoroutine(Attack(InstancePaw));
        CatPaw.SetInteger("Bool", 0);
    }

    IEnumerator WaitTime()
    {
        while(AllowPaw == true && HP.HP < 100)
        {
            yield return new WaitForSeconds(Random.Range(9, 19));
            if (HP.HP < 100f)
            {
                StartCoroutine(SpawnPaw());
            }
        }
    }

    public IEnumerator Attack(GameObject paw)
    {
        //paw.transform.localScale = new Vector3(100, 100, 100);
        float Fastness = 0.004f;
        while(paw.transform.position.y >= -27)
        {
            if (AnimAppleStart == false) {
                paw.transform.position -= new Vector3(0, Fastness, 0);
                Fastness += 0.0003f;
            }
            else 
            {
               paw.GetComponent<Animator>().rootPosition = new Vector3(0, 0, 0);
                paw.GetComponent<Animator>().SetInteger("Direction", Random.Range(1,3));
            }
            yield return null;
        }
        Object.Destroy(paw);
    }

    public IEnumerator CharacterDown()
    {
        foreach (GameObject target in Bubbles)
        {
            //target.GetComponent<Animator>().SetBool("Bust", true);
        }
        yield return null;
    }

    public IEnumerator _WaitBurst()
    {
        yield return new WaitForSeconds(0.45f);
        bursting();
    }

    public void bursting()
    {
        foreach (GameObject target in Bubbles)
        {
            GameObject.Destroy(target);
        }
    }

    public IEnumerator CharacterUnable()
    {
        Character.GetComponent<BubblerendererDoolhof>().stop = true;
        Character.GetComponent<Animator>().SetBool("Hit", true);
        yield return new WaitForSeconds(2);
    }

    public IEnumerator StartBubbles()
    {
        yield return new WaitForSeconds(2);

        Character.GetComponent<Animator>().SetBool("Hit", false);
        Character.GetComponent<BubblerendererDoolhof>().stop = false;
        StartCoroutine(Character.GetComponent<BubblerendererDoolhof>().BubbleAnimatie());
    }
}
