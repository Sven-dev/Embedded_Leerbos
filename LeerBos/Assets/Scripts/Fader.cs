using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Fades in the image the script is attached to.
public class Fader : MonoBehaviour
{
    private Image Image;
	// Use this for initialization
	void Start()
    {       
        Image = GetComponent<Image>();
        StartCoroutine(_Fade());
	}
	
    IEnumerator _Fade()
    {
        yield return new WaitForSeconds(1.5f);
        while(Image.color.a < 1)
        {
            Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, Image.color.a + 0.01f);
            yield return null;
        }
    }
}
