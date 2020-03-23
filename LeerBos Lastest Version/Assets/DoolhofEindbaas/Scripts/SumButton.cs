using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SumButton : MonoBehaviour, I_SmartwallInteractable {
    public string[] ItemsTotal;
    public GameObject Text;
    public string Item;
    public ControllerDolhof controller;
    public bool Number;
    public GameObject ThisItem;
    public Animator Animation;
    public AudioSource ButtonPush;
    public AudioSource Pop;
    public CatDefeated Cat;
    public IndicatorBubble IndiBubble;

    // Use this for initialization
    void Start () {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            if (controller.vraag.NumberOrNot == false)
            {
                Item = ItemsTotal[controller.Sign];
            controller.Sign++;
            if(controller.Sign == 13)
            {
                controller.Sign = 10;
            }
                
            }
            else
            {
                int Chance = Random.Range(1, 5);
                if (Chance == 1)
                {
                    Item = controller.vraag.Antwoord;
                }
                else
                {
                    Item = ItemsTotal[Random.Range(1, 10)];
                }
            }
            Text.GetComponent<TextMesh>().text = Item;
            Text.GetComponent<MeshRenderer>().sortingOrder = 1;
        
    }

    private void Update()
    {
    if (controller.vraag.Antwoord == Item)
    {
        if (this.tag == "DontDestroyTillSumCreated")
        {
            StartCoroutine(BubbleBursting());
        }
    }
        //if (Physics2D.OverlapCircle(transform.position, 0.3f));
        //{
        //    Animation.SetBool("Burst", true);
        //}
    }

    // Update is called once per frame
    public void Hit(Vector3 clickposition)
    {
        IndiBubble.WaitTime = 12;
            if (Item == controller.vraag.Antwoord)
            {
                StartCoroutine(CheckPoint());
                controller.AddSum(Item);
            }
            else
            {
            Animation.SetBool("Burst", true);
            StartCoroutine(CantUse2());
            }
        
    }


    //Pakt Goede Antwoord
    public IEnumerator CheckPoint()
    {
        this.tag = "DontDestroyTillSumCreated";

        GetComponent<BubbleAnimatiesDoolhof>().enabled = false;
        Vector3 StartingPosition = transform.position;
        Vector2 EndPosition = new Vector3(0f, 3.2f, 90);
        if (controller.SumList == 0 && SceneManager.GetActiveScene().name == "DolhoofEindbaas")
        {
            EndPosition = new Vector3(-1.4f, 3.2f, 90);
        }
        else if (controller.SumList == 3 && SceneManager.GetActiveScene().name == "DolhoofEindbaas")
        {
            EndPosition = new Vector3(1.4f, 3.2f, 90);
        }
        else
        {
            EndPosition = new Vector3(0f, 3.2f, 90);
        }
        float TotalTime = Time.deltaTime * 2f;
        StartingPosition = transform.position;
        while (Vector2.Distance(transform.position, EndPosition) >= 0.2)
        {
            transform.position = Vector2.Lerp(transform.position, EndPosition, TotalTime);
            yield return null;
        }
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        Cat.CatAttack();
        StartCoroutine(controller.Attack());
    }



    //Animaties als je item niet kan gebruiken
    public IEnumerator CantUse1()
    {
        for (int i = 0; i < 25; i++)
        {
            transform.position += new Vector3(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f), 0);
            yield return null;
        }
    }


    public IEnumerator CantUse2()
    {
        TextMesh text = Text.GetComponent<TextMesh>();
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

    IEnumerator BubbleBursting()
    {

        yield return new WaitForSeconds(2);
        Animation.SetBool("Burst", true);
    }

    public void Burst()
    {
        GameObject.Destroy(ThisItem);
    }
    public void RemoveText()
    {
        Text.GetComponent<TextMesh>().text = "";
    }
}
