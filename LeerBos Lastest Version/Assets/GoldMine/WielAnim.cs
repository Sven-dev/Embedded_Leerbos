using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WielAnim : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Anim());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator Anim()
    {
        if (transform.position.x > 0)
        {
            while (transform.position.x < 100)
            {
                transform.Rotate(Vector3.forward, 3);
                yield return null;
            }
        }
        else
        {
            while (transform.position.x < 100)
            {
                transform.Rotate(Vector3.back, 3);
                yield return null;
            }
        }
    }
}
