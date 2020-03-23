using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleIndicator : MonoBehaviour
{
    public bool Active;
    public AnimationBubble Animation = AnimationBubble.Scale;

    public GameObject Target;
    private RectTransform RT;
    private SpriteRenderer Image;

    // Use this for initialization
    void Start()
    {
        Active = false;
        RT = GetComponent<RectTransform>();
        Image = GetComponent<SpriteRenderer>();
    }

    public void Show()
    {
        Image.enabled = true;
        Active = true;

        StartCoroutine(_Move());
        switch (Animation)
        {
            case AnimationBubble.Scale:
                StartCoroutine(_Scale());
                break;
            case AnimationBubble.Fade:
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
        Image.transform.localScale = new Vector2(10, 10);
        yield return null;

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
        while (Active)
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

public enum AnimationBubble
{
    Fade,
    Scale
}