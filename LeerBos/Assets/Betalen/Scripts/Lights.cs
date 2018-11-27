using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lights : MonoBehaviour
{
    public bool Active;
    public float Speed;
    [Space]
    public Color Green;
    public Color Yellow;
    public Color Off;

    private Image[] LightList;

    private void Start()
    {
        LightList = GetComponentsInChildren<Image>();
    }

    //Fades the lights between the default color and the "off" color
    public void Flicker()
    {
        Active = true;
        StartCoroutine(_Blink(Yellow, Off));
    }

    //Fades the lights beteen the default color and the "correct" color
    public void Blink()
    {
        Active = true;
        StartCoroutine(_Blink(Yellow, Green));
    }

    //Stops any blinking lights
    public void Stop()
    {
        Active = false;
    }

    //Fades between 2 colors as long as active is true
    IEnumerator _Blink(Color a, Color b)
    {
        int signum = 1;
        float progress = 0;
        while(Active)
        {
            foreach (Image light in LightList)
            {
                light.color = Color.Lerp(a, b, progress += Time.deltaTime * Speed * signum);
                yield return null;
            }

            if (progress >= 1)
            {
                signum = -1;
            }
            if (progress <= 0)
            {
                signum = 1;
            }
        }

        foreach (Image light in LightList)
        {
            light.color = a;
        }
    }

    //Switches between 2 colors as long as active is true
    IEnumerator _Flicker(Color a, Color b)
    {
        while (Active)
        {
            foreach (Image light in LightList)
            {
                if (light.color == a)
                {
                    light.color = b;
                }
                else
                {
                    light.color = a;
                }
            }

            yield return new WaitForSeconds(0.1f);
        }

        foreach (Image light in LightList)
        {
            light.color = a;
        }
    }
}