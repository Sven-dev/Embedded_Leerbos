using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutlineBlinker : MonoBehaviour
{
    private bool Active;
    private Image Outline;
    public float BlinkRate;

	// Use this for initialization
	void Start ()
    {
        Active = false;
        Outline = GetComponent<Image>();
	}

    public void Blink()
    {
        StartCoroutine(_Blink());
    }

    IEnumerator _Blink()
    {
        int signum = 1;
        Active = true;
        while (Active)
        {
            Outline.color = new Color(Outline.color.r, Outline.color.g, Outline.color.b, Outline.color.a + BlinkRate * Time.deltaTime * signum);
            yield return null;

            if (Outline.color.a >= 1 || Outline.color.a <= 0)
            {
                signum = -signum;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
