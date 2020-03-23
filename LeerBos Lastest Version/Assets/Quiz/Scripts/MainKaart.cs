using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainKaart : MonoBehaviour, I_SmartwallInteractable
{
    [SerializeField] public QuizController controller;
    [SerializeField] public GameObject Card_Back;
    [SerializeField] private GameObject GameObjectText;
    [SerializeField] public Schaduw controller2;

    public void Hit(Vector3 clickposition)
    {
       if (Card_Back.activeSelf)
       {
            StartCoroutine(Vals());
            //StartCoroutine(AnimatieReveal());
            controller.CardRevealed(this);
            Card_Back.GetComponent<BoxCollider2D>();
            //SetActive(false);
            //Card_Back.SetActive(false);
        }
    }
    private IEnumerator Vals()
    {
        yield return new WaitForSeconds(0.5f);
        //Card_Back.SetActive(false);
    }

    public IEnumerator AnimatieReveal()
    {
        for (int i = 0; i < 25; i++)
        {
            transform.position += new Vector3(Random.Range(0, 10), Random.Range(0, 10), 0);

            yield return null;
        }
    }
    public IEnumerator AnimatieColorReveal()
    {
        TextMesh text = GetComponent<TextMesh>();
        SpriteRenderer SpriteColor = GetComponent<SpriteRenderer>();
        float ElapsedTime = 0.0f;
        float TotalTime = 1.5f;
        if (text != null)
        {
            while (text.color != Color.red || text.color != Color.black || ElapsedTime< TotalTime)
            {
                ElapsedTime += Time.deltaTime;
                text.color = Color.Lerp(text.color, Color.red, (ElapsedTime/TotalTime));
                yield return null;
            }
        }
        else if (SpriteColor != null)
        {
            while (controller2.ShadowColor != Color.yellow || controller2.ShadowColor != Color.green || ElapsedTime < TotalTime)
            {
                ElapsedTime += Time.deltaTime;
                controller2.ShadowColor = Color.Lerp(controller2.ShadowColor, Color.yellow, (ElapsedTime / TotalTime));
                yield return null;
            }
        }
    }

    public IEnumerator AnimatieUnReveal()
    {
        Vector3 originalpos = transform.position;
        for (int i = 0; i < 25; i++)
        {
            transform.position += new Vector3(Random.Range(-0.02f, 0.02f), Random.Range(-0.02f, 0.02f), 0);
            yield return null;
        }
        transform.position = originalpos;
    }
    public IEnumerator AnimatieColorUnReveal() { 
        TextMesh text = GetComponent<TextMesh>();
        SpriteRenderer SpriteColor = GetComponent<SpriteRenderer>();
        float ElapsedTime = 0.0f;
        float TotalTime = 1.5f;
        if (text != null)
        {
            while(text.color != Color.black || ElapsedTime < TotalTime)
            {
                ElapsedTime += Time.deltaTime;
                text.color = Color.Lerp(text.color, Color.black, (ElapsedTime / TotalTime));
                yield return null;
            }
        }
        else if (SpriteColor != null)
        {
            while (controller2.ShadowColor != Color.grey || ElapsedTime < TotalTime)
            {
                ElapsedTime += Time.deltaTime;
                controller2.ShadowColor = Color.Lerp(controller2.ShadowColor, Color.grey, (ElapsedTime / TotalTime));
                yield return null;
            }
        }
    }

    private int _id;
    public int id
    {
        get { return _id; }
    }
    public void ChangeSprite(Sprite image)
    {
        _id = id;

       // GetComponent<TextMesh>().text = controller.
    }
    public void ChangeId(int id)
    {
        _id = id;
    }
    public void ChangeText( int id, string someText)
    {
        _id = id;
        GetComponent<TextMesh>().text = controller.totaalAntwoorden[id];
    }

    public void Unreveal()
    {
        Card_Back.SetActive(true);
        GameObjectText.SetActive(true);
    }
}