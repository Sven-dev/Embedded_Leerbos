using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour {
    [SerializeField] GameObject druppel;
    [SerializeField] bool play;
    // Use this for initialization
    void Start () {
        StartCoroutine(updates(druppel));
	}
	
	// Update is called once per frame
	void Update () {

	}
    IEnumerator updates(GameObject item)
    {
        while (play == true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 5));
            GameObject Instancedruppel;
            Instancedruppel = Instantiate(item, new Vector2(Random.Range(-6, 6),5.5f), item.transform.rotation);
            Instancedruppel.transform.SetParent(gameObject.transform);
            Instancedruppel.transform.localScale = new Vector2(1, 1);
            Instancedruppel.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            Instancedruppel.GetComponent<AudioSource>().Play();
            Destroy(Instancedruppel, 4);
        }
    }
}
