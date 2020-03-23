using System.Collections;
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
        StartCoroutine(_Flicker(Off, Yellow));
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

        //Turns off the lights
        foreach (Image light in LightList)
        {
            light.color = a;
        }
    }

    //Stops any blinking lights
    public void Stop()
    {
        Active = false;
    }
}