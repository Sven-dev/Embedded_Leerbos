using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressAnimation : MonoBehaviour {
    [SerializeField] private Sprite[] Images;
    [SerializeField] private GameObject StarPoint;
    [SerializeField] private GameObject bar;
    private bool RotateBool;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public IEnumerator FallingStar()
    {
        GameObject Star;
        Star = Instantiate(StarPoint,StarPoint.transform.position, StarPoint.transform.rotation);
        Star.transform.localScale += new Vector3(2.5f,2.5f,1) ;
        Debug.Log("Working?");
        Star.SetActive(true);
        Star.GetComponent<SpriteRenderer>().sprite = Images[Random.Range(1,Images.Length)];
        Star.GetComponent<Rigidbody2D>().simulated = true;
        Star.transform.localScale = new Vector2(0.8f / bar.transform.localScale.x, 0.8f / bar.transform.localScale.x);
        RotateBool = true;
        StartCoroutine(Rotate(Star));
        yield return new WaitForSeconds(0.8f);
        Star.SetActive(false);
        Star.transform.localPosition = new Vector2(Star.transform.localPosition.x, 0);
        Star.GetComponent<Rigidbody2D>().simulated = false;
        RotateBool = false;
        Destroy(Star);
    }
    IEnumerator Rotate(GameObject StarPoint)
    {
        while (RotateBool == true)
        {
            StarPoint.transform.Rotate(0, 0, 1.5f);
            yield return null;
        }
    }
}
