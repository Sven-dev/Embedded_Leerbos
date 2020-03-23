using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class KlinkerScript : MonoBehaviour, I_SmartwallInteractable
{
    [SerializeField] public MainManager controller;
    // Use this for initialization
    public string KlinkerText;
    public List<float> Afstand = new List<float>();
    public float min;
    public MedeklinkerRenderer Renderer;
    public void Awake()
    {
        KlinkerText = GetComponent<TextMesh>().text;
    }


    //Script waar tekst kijkt als iets fout is.
    public void Hit(Vector3 clickposition)
    {
        KlinkerText = GetComponent<TextMesh>().text;
        controller.CheckOfKlinkerPast(this);
        Debug.Log(5);
    }
	//Animaties Bij een fout antwoord.
    public IEnumerator FoutEen()
    {
        for (int i = 0; i < 25; i++)
        {
            transform.position += new Vector3(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f), 0);
            yield return null;
        }
    }
    public IEnumerator FoutTwee()
    {
        TextMesh text = GetComponent<TextMesh>();
        text.color = Color.black;
        float ElapsedTime = 0.0f;
        float TotalTime = 0.4f;
        if (text != null)
        {
            while (text.color != Color.red || ElapsedTime < TotalTime)
            {
                ElapsedTime += Time.deltaTime;
                text.color = Color.Lerp(text.color, Color.red, (ElapsedTime / TotalTime));
                yield return null;
            }
            ElapsedTime = 0.0f;
            TotalTime = 0.9f;
            while (text.color != Color.black || ElapsedTime < TotalTime)
            {
                ElapsedTime += Time.deltaTime;
                text.color = Color.Lerp(text.color, Color.black, (ElapsedTime / TotalTime));
                yield return null;
            }
        }
    }
}
