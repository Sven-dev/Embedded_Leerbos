using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialIndicator : MonoBehaviour
{
    public bool Active;
    public Animation Animation = Animation.Scale;

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
        switch(Animation)
        {
            case Animation.Scale:
                StartCoroutine(_Scale());
                break;
            case Animation.Fade:
                StartCoroutine(_Fade());
                break;
        }
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
            progress += signum * Time.deltaTime;
            
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

        rt.sizeDelta = dimentionsMax;
    }

    IEnumerator _Fade()
    {
        Color Min = new Color(Image.color.r, Image.color.g, Image.color.b, 0);
        Color Max = new Color(Image.color.r, Image.color.g, Image.color.b, 1);

        int signum = 1;
        float progress = 0;
        while (Active)
        {
            Image.color = Color.Lerp(Min, Max, progress);
            progress += signum * Time.deltaTime;

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

            if (transform.position != Target.transform.position)
            {
                transform.position = Target.transform.position;
            }

            yield return null;
        }
    }
}

public enum Animation
{
    Fade,
    Scale
}