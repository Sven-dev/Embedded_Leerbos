using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneController : MonoBehaviour {

    public int gridRows = 4;
    public int gridCols = 2;
    public float offsetX = 7f;
    public float offsetY = 1f;
    public AudioSource matchSound;
    public AudioSource cardRevealSound;
    public AudioSource Bomb;
    public AudioSource SmallDebris;
    [SerializeField] private GameObject Explosion;

    [SerializeField] private MainCard originalCard;
    [SerializeField] private Sprite[] achterkantKaarten;
    [SerializeField] private Sprite[] Items;
    [SerializeField] private GameObject[] ItemsObjact;
    [SerializeField] private GameObject Vloer;
    [SerializeField] private GameObject Vuurtje;
    [SerializeField] private GameObject OnderMuur;
    [SerializeField] private GameObject[] AfterEffectExplosion;


    public Sprite[] Changepictures()
    {
        Sprite[] images;
         images = achterkantKaarten;
        return images;
    }


 private void Start()//Maakt gat in de muur ontzichtbaar
    {
        for(int i =0; i < AfterEffectExplosion.Length; i++)
        {
            AfterEffectExplosion[i].GetComponent<SpriteRenderer>().enabled = false;
            if(AfterEffectExplosion[i].GetComponent<PolygonCollider2D>() != null)
            {
                AfterEffectExplosion[i].GetComponent<PolygonCollider2D>().enabled = false;
                AfterEffectExplosion[i].GetComponent<Rigidbody2D>().simulated = false;
            }
        }
        //Zorgt dat animaties stop gezet worden
        Vuurtje.GetComponent<Animator>().enabled = false;
        Explosion.GetComponent<Animator>().enabled = false;

        //Zet kaarten op spel
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        Vector3 startPos = originalCard.transform.position;

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };

        if (sceneName == "Memory")
        {
            numbers = new int[] { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };
            gridRows = 3;
            gridCols = 4;
            offsetX = 2.9f;
            offsetY = 1.7f;
        }
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MainCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MainCard;
                }
                int index = j * gridCols + i;
                int id = numbers[index];
                Sprite[] images = Changepictures();
                card.ChangeSprite(id, images[id]);

                float postX = (offsetX * i) + startPos.x;
                float postY = (offsetY * j) + startPos.y;
                card.transform.position = new Vector3(postX, postY, startPos.z);
            }
        }
    }
    private int[] ShuffleArray(int[] numbers)//Kaarten schudden
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

    //Score en spelregels

    private MainCard _firstRevealed;
    private MainCard _secondRevealed;

    private int _score = 0;
    private int _win = -1;
    [SerializeField] private TextMesh scoreLabel;
    public float anim;
    public bool canReveal
    {
        get { return _secondRevealed == null; }
    }
    private IEnumerator Reveal(MainCard card)
    {
        cardRevealSound.Play();
        for (int i = 0; i < 18; i++)
            {
                card.transform.localScale += new Vector3(0.01f, 0.01f);
                card.transform.Rotate(0, 5, 0);
                yield return new WaitForEndOfFrame();
        }

            for (int j = 0; j < 18; j++)
            {
                card.transform.localScale -= new Vector3(0.01f, 0.01f);
                card.transform.Rotate(0, 5, 0);
                yield return new WaitForEndOfFrame();
        } 
    }

    private IEnumerator NotReveal(MainCard card)
    {
        cardRevealSound.Play();
        for (int i = 0; i < 15; i++)
        {
            card.transform.localScale += new Vector3(0.012f, 0.012f);
            card.transform.Rotate(0, -6, 0);
            yield return new WaitForEndOfFrame();
        }

        for (int j = 0; j < 15; j++)
        {
            card.transform.localScale -= new Vector3(0.012f, 0.012f);
            card.transform.Rotate(0, -6, 0);
            yield return new WaitForEndOfFrame();
        }
    }

    public void CardRevealed(MainCard card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
            StartCoroutine(Reveal(_firstRevealed));

        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(Reveal(_secondRevealed));

            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if(_firstRevealed.id == _secondRevealed.id)//Code als je goed hebt gegooid
        {
           plaatje = _firstRevealed.GetComponent<SpriteRenderer>().sprite;
            _win++;
            matchSound.Play();
        }
        else//Code als je het fout hebt gegooid
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(NotReveal(_firstRevealed));
            StartCoroutine(NotReveal(_secondRevealed));
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
            _score++;
        }
        if (_firstRevealed.id == _secondRevealed.id)//Animatie als je iets goed doet
        {
            StartCoroutine(Animatie());
        }
        _firstRevealed = null;
        _secondRevealed = null;
    }

    //Alle code voor de animaties
    public IEnumerator Animatie()
    {
        MainCard animatiekaartje;
        MainCard animatiekaartje2;
        animatiekaartje = _firstRevealed;
        animatiekaartje2 = _secondRevealed;
        //Maakt animatie van het plaatje
        if (animatiekaartje.GetComponent<SpriteRenderer>().sprite = plaatje)
        {
            for (int i = 0; i < 6; i++)
            {
                if (_win == i)
                {
                    location = bakjes[i];
                }
            }
            while (animatiekaartje2.transform.localScale.y != 0.8f)
            {
                yield return null;
            }
            while (animatiekaartje2.transform.position.y > -4.42f || animatiekaartje.transform.position.y > -4.42f)
            {
                if (animatiekaartje.transform.localScale.x < -0.2f ) {
                    animatiekaartje.transform.localScale -= new Vector3(-0.035f, 0.035f);
                    animatiekaartje2.transform.localScale -= new Vector3(-0.035f, 0.035f);
                }
                animatiekaartje.transform.position = Vector2.MoveTowards(animatiekaartje.transform.position, location, 12f * Time.deltaTime);
                animatiekaartje2.transform.position = Vector2.MoveTowards(animatiekaartje2.transform.position, location, 12f * Time.deltaTime);
            
                yield return null;
            }
            


            for (int i = 0; i < 6; i++)//Maakt animatie voor de losse objects
            {
                if (_win == i)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (animatiekaartje.GetComponent<SpriteRenderer>().sprite == achterkantKaarten[j])
                        {
                            ItemsObjact[i].GetComponent<SpriteRenderer>().sprite = Items[j];
                            while (ItemsObjact[i].transform.localScale.y < 0.45)
                            {
                                ItemsObjact[i].transform.localScale += new Vector3(0.01f, 0.01f);
                                ItemsObjact[i].transform.position += new Vector3(0f, 0.02f);
                                yield return null;
                            }
                        }
                        
                    }
                }
            }
        }
        if (_win == Changepictures().Length - 1)
        {
            StartCoroutine(Disappear());
        }
    }
    private Sprite plaatje;
    private Vector2 location;
    public IEnumerator Disappear()
    {
        Bomb.Play();
        yield return new WaitForSeconds(0.50f);
        Vuurtje.GetComponent<Animator>().enabled = true;
        while (Vloer.transform.position.y <= -3.75f)
        {
            //Vloer.transform.localScale += new Vector3(0, 0.2f);
            Vuurtje.transform.localScale -= new Vector3(0.002f, 0.002f);
            Vloer.transform.position += new Vector3(0, 0.02f);
            Vuurtje.transform.position -= new Vector3(0, 0.0015f);
            if (Vuurtje.transform.position.x >= -0.15)
            {
                Vuurtje.transform.position -= new Vector3(0.010f, 0.00f);
            }
            else
            {
                Vuurtje.transform.position += new Vector3(0.003f, 0.00f);
            }
            yield return null;
        }
        OnderMuur.SetActive(false);
        Explosion.GetComponent<Animator>().enabled = true;
        //Vuurtje.GetComponent<Animator>().enabled = false;
        //Vuurtje.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.33f);
        SmallDebris.Play();
        for (int i = 0; i < AfterEffectExplosion.Length; i++)
        {
            AfterEffectExplosion[i].GetComponent<SpriteRenderer>().enabled = true;
            if (AfterEffectExplosion[i].GetComponent<PolygonCollider2D>() != null)
            {
                AfterEffectExplosion[i].GetComponent<PolygonCollider2D>().enabled = true;
                AfterEffectExplosion[i].GetComponent<Rigidbody2D>().simulated = true;
            }
        }
        yield return new WaitForSeconds(0.33f);
        DestroyImmediate(Explosion);
        yield return null;
        Victory();
    }
    [SerializeField] public Vector2[]  bakjes;
    //[SerializeField] private GameObject touw;
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


//-5.03 -2.32
