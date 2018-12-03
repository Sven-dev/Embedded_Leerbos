using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialIndicator : MonoBehaviour
{
    public bool Active;

    public GameObject Target;
    private RectTransform RT;
    private Image Image;

	// Use this for initialization
	void Start ()
    {
        Active = false;
        RT = GetComponent<RectTransform>();
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

    //Scales the object down and up
    IEnumerator _Scale()
    {
        RectTransform rt = GetComponent<RectTransform>();
        Vector2 dimentionsMax = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y);
        Vector2 dimentionsMin = new Vector2(rt.sizeDelta.x - 50, rt.sizeDelta.y - 50);

        int signum = -1;
        float progress = 1;

        while (Active)
        {
            rt.sizeDelta = Vector2.Lerp(dimentionsMin, dimentionsMax, progress);
            progress += 0.025f * signum;
            
            if (progress >= 1)
            {
                progress = 1;
                signum = -1;
            }
            else if (progress <= 0)
            {
                progress = 0;
                signum = 1;
            }

            yield return null;
        }

        rt.sizeDelta.Set(dimentionsMax.x, dimentionsMax.y);
    }

    //Makes sure the object is at the position of the target, without being a child-object
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