using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubblerenderer : MonoBehaviour {
    public GameObject Bubble;
    private GameObject InstanceBubble;
    public List<Vector3> posities = new List<Vector3>();
    public float spawnWait;
    public float spawnLeastWait;
    public float spawnMostWait;
    public bool stop;
    public Sprite pop;
    // Use this for initialization
    void Start () {
        StartCoroutine(BubbleAnimatie());
    }
	
	// Update is called once per frame
	void Update () {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
        
	}
    IEnumerator BubbleAnimatie()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 2.5f));

        while (!stop)
        {
            InstanceBubble = Instantiate(Bubble, posities[Random.Range(0, posities.Count)], Bubble.transform.rotation);
            yield return new WaitForSeconds(Random.Range(0.5f,2.5f));
            InstanceBubble.GetComponent<SpriteRenderer>().sprite = pop;
            yield return new WaitForSeconds(0.05f);
            Destroy(InstanceBubble);
        }
    }
}
