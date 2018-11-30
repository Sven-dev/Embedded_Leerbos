using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialIndicator : MonoBehaviour
{
    public bool Active;

    public GameObject Target;
    private Collider2D Collider;
    private Image Image;

	// Use this for initialization
	void Start ()
    {
        Active = false;
        Collider = Target.GetComponent<Collider2D>();
        Image = GetComponent<Image>();
	}

    public void Show()
    {
        Image.enabled = true;
        Active = true;

        StartCoroutine(_Move());
        StartCoroutine(_Scale());
    }

    public void Hide()
    {
        Active = false;
        Image.enabled = false;
    }

    IEnumerator _Scale()
    {
        transform.localScale = Vector3.one;

        while (Active)
        {
            while (transform.localScale.x > 0.8f)
            {
                transform.localScale -= Vector3.one * Time.deltaTime * 0.25f;
                yield return null;
            }

            while (transform.localScale.x < 1)
            {
                transform.localScale += Vector3.one * Time.deltaTime * 0.25f;
                yield return null;
            }
        }
    }

    IEnumerator _Move()
    {
        while(Active)
        {
            if (Target == null)
            {
                Destroy(gameObject);
            }

            transform.position = Target.transform.position;
            yield return null;
        }
    }
}