using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour {
    [SerializeField] CalenderManager Controller;
	// Use this for initialization
	void Start () {
        gameObject.transform.SetSiblingIndex(1);
        StartCoroutine(Wait());
	}
	
	// Update is called once per frame
	IEnumerator Wait () {
        //if (Controller.Loose == true)
        //{
        while(gameObject.GetComponent<Rigidbody2D>().constraints != RigidbodyConstraints2D.None)
        {
            yield return null;
        }
        gameObject.transform.SetAsLastSibling();
        Destroy(gameObject, 3);
        StartCoroutine(_rotate());
        //   Controller.Loose = false;
	}
    IEnumerator _rotate()
    {
        float direction = Random.Range(-1f, 1f);
        while (gameObject.transform.position.y > -50)
        {
            gameObject.transform.Rotate(Vector3.forward * direction);
            yield return null;
        }
    }
    private void Update()
    {
        if (gameObject.GetComponent<Rigidbody2D>().constraints == RigidbodyConstraints2D.None)
        {
            gameObject.transform.SetAsLastSibling();
        }
    }
}
