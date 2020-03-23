using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CalenderManager : MonoBehaviour {
    public Question[] Questions;
    public Text text;
    public Question question;
    public VictoryScript VictoryCanvas;
    public int score = 0;
    public bool Loose;
    [SerializeField] GameObject blockExample;
    [SerializeField] GameObject _Background;
    [SerializeField] GameObject _Canvas;
    [SerializeField] AudioClip _Audioplay;
    Vector2[] Areas =
    {
        new Vector2(-4.5f, -4),
        new Vector2(-1.5f,-4),
        new Vector2(1.5f, -4),
        new Vector2(3.5f, -4),
    };
    string[] Dates =
    {
        "1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","17","18","19","20","21","22","23","24","25","26","27","28","29","30"
    };
    string[] Days =
    {
        "Maandag","Dinsdag","Woensdag","Donderdag","Vrijdag","Zaterdag","Zondag"
    };
    string[] Months =
{
        "Januari","Februari","Maart","April","Mei","Juni","Juli","Augustus","September","Oktober","November","December"
    };
    // Use this for initialization
    void Start () {
        score = 0;
        NewQuestion();
        //if(SceneManager.GetActiveScene().name == "DagenInEenMaand")
        //{
        //    StartCoroutine(ThreeQuestions());
        //}
    }
	
	// Update is called once per frame
	void Update () {

	}


    public void NewQuestion()
    {
        if (score > 3)
        {
            Victory();
        }
        else
        {
            Loose = true;

            if(score>0)
            {
                GameObject Instancing;
                Instancing = Instantiate(_Background, _Background.transform.position, _Background.transform.rotation);
                Instancing.transform.SetParent(_Canvas.transform);
                Instancing.transform.localScale = new Vector2(1, 1);
                _Background.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

                foreach (GameObject RemoveInstance in GameObject.FindGameObjectsWithTag("Instance"))
                {
                    RemoveInstance.transform.tag = "Kart";
                    RemoveInstance.transform.SetParent(_Background.transform);
                }
                _Background = Instancing;
                _Background.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                gameObject.GetComponent<AudioSource>().clip = _Audioplay;
                gameObject.GetComponent<AudioSource>().Play();
            }
            else
            {
                _Background.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }


            text = _Background.GetComponentInChildren<Text>();
            question = Questions[Random.Range(0, Questions.Length - 1)];
            text.text = question.mainQuestion;
            StartCoroutine(ThreeQuestions());
        }
    }

    public IEnumerator ThreeQuestions()
    {
        int type = 0;
        int ShittyLoop = 0;
        List<Question> threeAnswers = new List<Question>();
        threeAnswers.Add(question);
        int AreaPlace =0;
        for(int i = 1; i<3;i++)
        { 
            threeAnswers.Add(Questions[Random.Range(0, Questions.Length-1)]);
            while(threeAnswers[i] == threeAnswers[0])
            {
                threeAnswers[i] = Questions[Random.Range(0, Questions.Length - 1)];
            }
        }

        GameObject Block;
        while(threeAnswers.Count > 0)
        {
            Question Allquestion = threeAnswers[Random.Range(0, threeAnswers.Count)];
            Block = Instantiate(blockExample, Areas[AreaPlace], blockExample.transform.rotation);
            Block.tag = "Instance";
            Block.transform.SetParent(_Background.transform.parent);
            Block.transform.localScale = new Vector2(1, 1);
            Block.GetComponentInChildren<Text>().text = Allquestion.answer;

            Block.GetComponent<Date>().answer = Allquestion.answer;
            threeAnswers.Remove(Allquestion);
            print(threeAnswers.Count);
            AreaPlace++;
            yield return null;
        }

        List<GameObject> texts = new List<GameObject>();
        foreach (GameObject t in GameObject.FindGameObjectsWithTag("Instance"))
        {
            texts.Add(t);
        }
        for (int i = 0; i < Days.Length; i++)
        {
            if (Days[i] == question.answer)
            {
                type = 1;
            }
        }
        
        foreach (GameObject RemoveInstance in GameObject.FindGameObjectsWithTag("Instance"))
        {
            if (RemoveInstance.GetComponent<Date>().answer != question.answer)
            {
                if (type == 0)
                {
                    RemoveInstance.GetComponentInChildren<Text>().text = Dates[Random.Range(0, Dates.Length)];
                }
                else
                {
                   RemoveInstance.GetComponentInChildren<Text>().text = Days[Random.Range(0, Days.Length)];
                }
            }
        }
        
        if (type == 0)
        {
            while (texts[0].GetComponentInChildren<Text>().text == texts[1].GetComponentInChildren<Text>().text ||
                texts[0].GetComponentInChildren<Text>().text == texts[2].GetComponentInChildren<Text>().text ||
                texts[1].GetComponentInChildren<Text>().text == texts[2].GetComponentInChildren<Text>().text)
            {
                Debug.Log("Redone");
                for (int i = 0; i < texts.Count; i++)
                {
                    if (texts[i].GetComponent<Date>().answer != question.answer)
                    {
                        texts[i].GetComponentInChildren<Text>().text = Dates[Random.Range(0, Dates.Length)];
                    }
                }
                Debug.Log(texts[0].GetComponentInChildren<Text>().text + texts[1].GetComponentInChildren<Text>().text + texts[2].GetComponentInChildren<Text>().text);

                ShittyLoop++;
                if (ShittyLoop > 100)
                {
                    break;
                }
            }
        }
        else
        {
            //threeAnswers[1].answer = Days[Random.Range(0, Days.Length)];
            //threeAnswers[2].answer = Days[Random.Range(0, Days.Length)];
            while (texts[0].GetComponentInChildren<Text>().text == texts[1].GetComponentInChildren<Text>().text ||
                texts[0].GetComponentInChildren<Text>().text == texts[2].GetComponentInChildren<Text>().text ||
                texts[1].GetComponentInChildren<Text>().text == texts[2].GetComponentInChildren<Text>().text)
            {
                Debug.Log("Redo");
                for (int i = 0; i < texts.Count; i++)
                {
                    if (texts[i].GetComponent<Date>().answer != question.answer)
                    {
                        texts[i].GetComponentInChildren<Text>().text = Days[Random.Range(0, Days.Length)];
                    }
                }
                Debug.Log(texts[0].GetComponentInChildren<Text>().text + texts[1].GetComponentInChildren<Text>().text + texts[2].GetComponentInChildren<Text>().text);
                ShittyLoop++;
                if (ShittyLoop > 100)
                {
                    break;
                }
            }
        }
    }
    private void Victory()
    {
        StartCoroutine(_Victory());
    }

    IEnumerator _Victory()
    {
        yield return new WaitForSeconds(0.5f);
        VictoryCanvas.Enable();
    }
}
