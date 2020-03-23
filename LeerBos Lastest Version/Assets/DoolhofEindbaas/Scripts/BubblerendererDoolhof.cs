using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubblerendererDoolhof : MonoBehaviour {
    public GameObject Bubble;
    private GameObject InstanceBubble;
    public Vector3 posities;
    public GameObject Character;
    public ControllerDolhof controller;
    public float spawnWait;
    public float spawnLeastWait;
    public float spawnMostWait;
    public bool stop;
    public Sprite pop;

    private void Awake()
    {
        Character.GetComponent<SpriteRenderer>().sortingOrder = 2;
    }
    // Use this for initialization
    void Start () {
        //StartCoroutine(BubbleAnimatie());
    }
	
	// Update is called once per frame
	void Update () {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
        
	}
    public IEnumerator BubbleAnimatie()
    {
       
        yield return new WaitForSeconds(Random.Range(0.5f, 2.5f));
        posities = Character.transform.position;
        posities += new Vector3(0.9f, 0.2f, 0.2f);
        
        InstanceBubble = Instantiate(Bubble, posities, Bubble.transform.rotation);
        while (!stop)
        {
            //GameObject[] Bubbels = GameObject.FindGameObjectsWithTag("Bubble");
            //For
            if (InstanceBubble == null)
            {
                yield return new WaitForSeconds(Random.Range(0.5f, 1f));
                InstanceBubble = Instantiate(Bubble, posities, Bubble.transform.rotation);
            }

            else if (Vector2.Distance(InstanceBubble.transform.position,posities) > 1.8f)
            { 
                InstanceBubble = Instantiate(Bubble, posities, Bubble.transform.rotation);
            }
            if(controller.SumOrNumber == false)
            { 
                controller.SumOrNumber = true;
            }
            else
            {
                controller.SumOrNumber = false;
            }
            yield return new WaitForSeconds(Random.Range(0.7f,1.2f));
            posities = Character.transform.position;
            posities += new Vector3(0.9f, 0.2f, 0.2f);
        }
    }
}
