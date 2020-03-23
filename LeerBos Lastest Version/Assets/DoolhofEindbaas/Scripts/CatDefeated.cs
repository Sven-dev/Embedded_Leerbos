using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatDefeated : MonoBehaviour {
    public ControllerDolhof Score;
    public GameObject SpawnSumAttack;
    public TextMesh TotalScore;
    public GameObject Character;
    [SerializeField] private Material CatIdle;
    [SerializeField] private Material CatHurt;
    [SerializeField] private GameObject Cat;
    public GameObject LeftEye;
    public GameObject RightEye;
    private Vector3 LeftEyeSocket;
    private Vector3 RightEyeSocket;
    private bool catEyesClosed = false;
    public bool CatAttacked = false;
    public bool CatAttacked2 = false;

    public AudioSource WhooshSmall;
    public AudioSource CatHurtSound;
    public AudioSource CatDown;
    public AudioSource CatHit;

    [SerializeField] private Sprite EyeClosed;
    [SerializeField] private Sprite EyeOpen;
    [SerializeField] private CatDizzy BeingHit;
    public bool Bool = false;
	// Use this for initialization

	void Start ()
    {
        SpawnSumAttack.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {

        LeftEyeSocket = new Vector3(-0.62f, 0.11f, 99);
        RightEyeSocket = new Vector3(0.62f, 0.11f, 99);

        if (catEyesClosed == false && Cat.transform.position.y >= -5)
        {
            Vector3 CharacterPosition = Character.transform.position;
            float angle = Mathf.Atan2(CharacterPosition.y * 3f, CharacterPosition.x) * Mathf.Rad2Deg;
            Cat.transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
            LeftEye.transform.position = LeftEyeSocket += new Vector3(CharacterPosition.x * 0.025f, -0.3f, 0);
            RightEye.transform.position = RightEyeSocket += new Vector3(CharacterPosition.x * 0.025f, -0.3f, 0);
        }
        else if(catEyesClosed == true)
        {
            LeftEye.GetComponent<SpriteRenderer>().sprite = EyeClosed;
            RightEye.GetComponent<SpriteRenderer>().sprite = EyeClosed;
            if (Score.HP >99)
            {
                _CatDefeat();
            }
        }
    }

    private void _CatDefeat()
    {
        StartCoroutine(CatDefeat());
    }

    IEnumerator CatDefeat()
    {
        Character.GetComponent<BubblerendererDoolhof>().stop = true;
        if (CatDown.enabled == false)
        {
            CatDown.enabled = true;
            Victory();
        }
        float speed = 0.0001f;
        while (Cat.transform.position.y >= -10)
        {
            Cat.transform.position -= new Vector3(0.00f,speed,0.00f);
            speed += 0.0003f;
            yield return null;
        }
    }

    public void CatAttack()
    {
        Bool = true;
        SpawnSumAttack.SetActive(true);
        WhooshSmall.Play();
        TotalScore.text = "";
        StartCoroutine(AttackBubbleTowardsCat());
    }

    IEnumerator AttackBubbleTowardsCat()
    {
        float speed = 0.05f;
        SpawnSumAttack.transform.position = new Vector3(0, 4.5f, 0);
        while (SpawnSumAttack.transform.position.y > 2.15)
        {
            SpawnSumAttack.transform.position -= new Vector3(0, speed, 0);
            speed += 0.005f ;
            yield return null;
        }
        SpawnSumAttack.SetActive(false);
        StartCoroutine(CatPain());
    }

    public IEnumerator CatPain()
    {
        GetComponent<Animator>().enabled = true;
        CatHurtSound.Play();
        CatHit.Play();
        catEyesClosed = true;
        //Cat.GetComponent<Renderer>().material = CatHurt;
        Character.GetComponent<BubblerendererDoolhof>().stop = true;
        GetComponent<Animator>().SetBool("Dizzy", true);
        StartCoroutine(BeingHit.CatHit());
        while(CatAttacked2 == true)
        {
            //StopDizzy();
            //StopCoroutine(CatPain());
            yield return null;
        }
        //yield return new WaitForSeconds(8);
        StopDizzy();
    }

    void StopDizzy()
    {
        GetComponent<Animator>().SetBool("Dizzy", false);
        catEyesClosed = false;
        Score.CatDizzy = false;
        Score.vraag = Score.AlleVragen[Random.Range(0, Score.AlleVragen.Length)];
        Score.SumText.text = Score.vraag.Vraag;
        //Cat.GetComponent<Renderer>().material = CatIdle;
        LeftEye.GetComponent<SpriteRenderer>().sprite = EyeOpen;
        RightEye.GetComponent<SpriteRenderer>().sprite = EyeOpen;
        Bool = false;
        Character.GetComponent<BubblerendererDoolhof>().stop = false;
        StartCoroutine(Character.GetComponent<BubblerendererDoolhof>().BubbleAnimatie());
        GetComponent<Animator>().enabled = false;
        CatAttacked = false;
        //CatAttacked2 = false;
        
        StopCoroutine(CatPain());
        StopCoroutine(Score.Attack());
        StopCoroutine(Score.CatIsDizzy());
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
