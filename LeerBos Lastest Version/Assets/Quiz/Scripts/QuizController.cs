using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizController : MonoBehaviour
{
    public AudioSource trueMatch;
    public AudioSource falseMatch;
    public AudioClip[] audioSources;

    public MainKaart originalCard;
    [SerializeField] private MainKaart _NameObject;
    [SerializeField] private Sprite[] images;
    [SerializeField] private TextMesh Namen;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private string targetMessage;
    [SerializeField] private TextMesh TotalScore;
    public Vraag[] vragen;
    public Vraag[] Dieren;
    public Vraag[] Fruit;
    public Vraag[] Kleuren;
    public Vraag[] Meubelier;
    public GameObject[] TextClones;
    [SerializeField] private GameObject Picture;

    public List<string> totaalAntwoorden = new List<string>();
    public List<Vraag> WillekeurigeVraag = new List<Vraag>();
    public int randomIndex;
    public string Antwoord;

    //Animaties van de tovenaar
    public Animator Animations;
    public GameObject RightHand;
    public GameObject LeftHand;
    public GameObject Bang;
    public GameObject Glow;
    public Animator LeftArm;
    public AudioSource FoutAntwoord;
    // Pakt alle vragen op
    public void gameStart(int level)
    {
        switch (level)
        {
            case 0:
                vragen = Meubelier;
                break;
            case 1:
                vragen = Kleuren;
                break;
            case 2:
                vragen = Fruit;
                break;
            case 3:
                vragen = Dieren;
                break;
        }
        foreach (GameObject Menus in GameObject.FindGameObjectsWithTag("MenuItem"))
        {
            GameObject.Destroy(Menus);
        }
        _NameObject.transform.Translate(Vector2.left*10);
        for (int i = 0; i < vragen.Length; i++)
            WillekeurigeVraag.Add(vragen[i]);
            begin();
        
    }

    void begin()
    {
        Namen.GetComponent<TextMesh>().color = Color.black;
        winText.SetActive(false);
        StopAllCoroutines();
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        totaalAntwoorden.Clear();
        randomIndex = Random.Range(0, WillekeurigeVraag.Count);
        string Vraag = vragen[randomIndex].TrueQuestion;
        Antwoord = vragen[randomIndex].TrueAnswer;
        string[] antwoorden = vragen[randomIndex].FalseAnswers;
        totaalAntwoorden.Add(antwoorden[0]);
        totaalAntwoorden.Add(antwoorden[1]);
        totaalAntwoorden.Add(antwoorden[2]);
        totaalAntwoorden.Add(Antwoord);
        WillekeurigeVraag.Remove(vragen[randomIndex]);

        Picture.GetComponent<SpriteRenderer>().sprite = vragen[randomIndex].Image;

        if (_score > 0)
        {
            StartCoroutine(ArmsMovementNew());
        }
        StartCoroutine(StopBang());

        Vector3 startPos = originalCard.transform.position;
        int[] numbers = { 0, 1, 2, 3 };
        Vector3 startPos2 = _NameObject.transform.position;
        int[] numbersText = { 0, 1, 2, 3 };
        int gridCols = 2;
        int gridRows = 2;
        float offsetX = 5.2f;
        float offsetY = 1.9f;

        
            MainKaart card;
            card = originalCard;
            originalCard.Unreveal();
            card.ChangeId(Random.Range(0,3));
            card.GetComponent<TextMesh>().text = Vraag;
            originalCard.Card_Back.SetActive(false);

            numbersText = ShuffleArray(numbers);


        //Zet vragen in het spel
        for (int j = 0; j < gridCols; j++)
        {
            for (int i = 0; i < gridRows; i++)
            {
                MainKaart card2;
                if (i == 0 && j == 0)
                {
                    card2 = _NameObject;
                }
                else
                {
                    card2 = Instantiate(_NameObject) as MainKaart;
                }
                int index = i * gridCols + j;

                int id = numbersText[index];
                card2.ChangeText(id, totaalAntwoorden[id]);
                card2.ChangeId(id);
                float postX = (offsetX * j) + startPos.x;
                float postY = (offsetY * i) + startPos2.y;
                card2.transform.position = new Vector3(postX, postY, startPos2.z);
            }
        }

    }
    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }



    public MainKaart _firstRevealed;
    public int _score = 0;


    public void CardRevealed(MainKaart card)
    {
        StartCoroutine(card.AnimatieColorReveal());
        _firstRevealed = card;
        StartCoroutine(CheckMatch());
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.GetComponent<TextMesh>().text == Antwoord)
        {
            if(Animations.GetInteger("Reaction") == 0)
            {
                Animations.SetInteger("Reaction", 1);
            }
            else
            {
                Animations.SetInteger("Reaction", 2);
            }
            LeftArm.SetInteger("Antwoord", 1);

            StartCoroutine(ArmsMovementTrue());
            yield return new WaitForSeconds(0.4f);
            LeftArm.SetInteger("Antwoord", 0);

            _NameObject.tag = "Respawn";
            GameObject[] TextClone = GameObject.FindGameObjectsWithTag("Respawn");
            RandomTrueMatch();
            _score++;
            TotalScore.GetComponent<TextMesh>().text = "Score: " + _score;
            
            StopAllCoroutines();


            if (_score == 4)
            {
                winText.SetActive(true);
                winText.transform.position -= new Vector3(0.00f, 0.00f, 5.0f);
                if (targetObject != null)
                {
                    targetObject.SendMessage(targetMessage);
                }
                Destroy(originalCard);
                Destroy(TextClone[0]);
                Destroy(TextClone[1]);
                Destroy(TextClone[2]);
                Destroy(Namen);
                StartCoroutine(StopBang());
                Victory();
            }
            else
            {
                Debug.Log("bye");
                _firstRevealed.Unreveal();
                Destroy(TextClone[0]);
                Destroy(TextClone[1]);
                Destroy(TextClone[2]);
                begin();
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            FoutAntwoord.Play();
            Animations.SetInteger("Reaction", 0);
            falseMatch.Play();
            _firstRevealed.Unreveal();
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(_firstRevealed.AnimatieColorUnReveal());
            StartCoroutine(_firstRevealed.AnimatieUnReveal());
        }

        _firstRevealed = null;
    }


    void RandomTrueMatch()
    {
        trueMatch.clip = audioSources[Random.Range(0, audioSources.Length)];
        trueMatch.Play();
    }

    public void Herstart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public IEnumerator ArmsMovementTrue()
    {
        for (int i = 0; i < 25; i++)
        {
            Debug.Log("hi");
            LeftHand.transform.Rotate(Vector3.forward, 3);
            if(i > 14 && Bang.GetComponent<SpriteRenderer>().color.a < 1f)
            {
                Bang.GetComponent<SpriteRenderer>().color += new Color(1, 1, 1, 0.1f);
            }
            if (i > 14 && Glow.GetComponent<SpriteRenderer>().color.a < 1f)
            {
                Glow.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.2f);
            }
            yield return null;
        }
    }
    public IEnumerator ArmsMovementNew()
    {
        for (int i = 0; i < 25; i++)
        {
            Debug.Log("hi");
            LeftHand.transform.Rotate(Vector3.back, 3);
            Debug.Log("oke");
            yield return null;
        }
    }
    public IEnumerator StopBang()
    {
        while (Bang.GetComponent<SpriteRenderer>().color.a > 0f)
        {
            Bang.GetComponent<SpriteRenderer>().color -= new Color(1, 1, 1, 0.1f);
            Debug.Log("waarom");
            if (Glow.GetComponent<SpriteRenderer>().color.a > 0f)
            {
                Glow.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.2f);
            }
            yield return null;
        }
    }
    public VictoryScript VictoryCanvas;
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
