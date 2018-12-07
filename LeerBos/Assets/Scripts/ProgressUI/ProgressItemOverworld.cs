using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressItemOverworld : MonoBehaviour
{
    public Transform Target;
    public int MoveSpeed;
    private Image[] images;

    // Use this for initialization
    void Start()
    {
        images = GetComponentsInChildren<Image>();
        transform.localScale = Vector3.zero;
    }

    public void PlayAnimation()
    {
        StartCoroutine(_Grow());
    }
    
    private IEnumerator _Grow()
    {
        float i = 0;
        while (i < 1)
        {
            i += Time.deltaTime/1;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, i);
            yield return null;
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(_MoveTowardsTarget());
    }

    private IEnumerator _MoveTowardsTarget()
    {
        while (transform.position != Target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position, MoveSpeed * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(_Fade());
    }

    private IEnumerator _Fade()
    {
        float i = 0;

        while (images[0].color.a > 0)
        {
            foreach(Image img in images)
            {
                Color tempColor = img.color;
                tempColor.a -= 1.2f * Time.deltaTime;
                img.color = tempColor;
            }

            i += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.3f, 1.3f),i);

            yield return null;
        }
    }
}
