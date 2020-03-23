using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapReadingMainManager : MonoBehaviour {
    [SerializeField] public Map[] Maps;
    [SerializeField] private Image mapRenderer;
    [SerializeField] private Image photo;
    [SerializeField] public GameObject Location;
    [SerializeField] AudioClip photoup;
    [SerializeField] AudioClip photodown;
    public Map UsingMap;
    public Photo UsingPhoto;
    public Vector2 pos;
    public Image beam;
    Vector3 MapVector;
    Vector3 PhotoVector1;
    Vector3 PhotoVector2;
    AudioSource photosound;
    int Score = -1;
    public bool wait;

    // Use this for initialization
    void Start() {
        wait= false;
         photosound = gameObject.GetComponent<AudioSource>();
        MapVector = mapRenderer.transform.position;
        PhotoVector1 = photo.transform.position;
        PhotoVector2 = photo.transform.position + new Vector3(0, 0.8f, 0);
        mapRenderer.transform.position = MapVector + new Vector3(0, 6.7f, 0);
        photo.transform.position = PhotoVector1 + new Vector3(0, 6.5f, 0);
        PlaceMap();
    }

    // Update is called once per frame
    void Update() {

    }

    void SpawnArrows()
    {
        GameObject Arrows;
        for (int i = 0; i < UsingMap.Photos.Length; i++)
        {
            Arrows = Instantiate(Location, UsingMap.Photos[i]._Coordinate, Quaternion.Euler(UsingMap.Photos[i]._Rotation));
            Arrows.transform.SetParent(mapRenderer.transform, true);
            Arrows.transform.localScale = new Vector3(1f, 1f, 0);
            Arrows.transform.position += new Vector3(0, 6.7f, 0);
            Arrows.tag = "Arrow";
        }
    }
    void PlaceMap()
    {
        UsingMap = Maps[Random.Range(0, Maps.Length)];
        mapRenderer.sprite = UsingMap.map;
        UsingPhoto = UsingMap.Photos[Random.Range(0, UsingMap.Photos.Length)];
        photo.sprite = UsingPhoto._img;
        if (UsingPhoto.vertical == true)
        {
            photo.transform.localScale = new Vector3(1, 1, 1);
            beam.transform.localScale = new Vector3(1, 1, 1);
            beam.transform.localPosition = new Vector2(0, -300);
        }
        else
        {
            photo.transform.localScale = new Vector3(1.6f, 0.7f, 1);
            beam.transform.localScale = new Vector3(0.7f, 1.6f, 1);
            beam.transform.localPosition = new Vector2(0, -400f);
        }
        SpawnArrows();
        pos = UsingPhoto._Coordinate;
        StartCoroutine(NextQuestion2());
        StartCoroutine(NextPhoto2());
        photosound.clip = photodown;
        photosound.Play();
        Score++;
    }

    void EmptyMap()
    {

        mapRenderer.sprite = null;
        photo.sprite = null;
        GameObject[] Arrows = GameObject.FindGameObjectsWithTag("Arrow");
        for (int i = 0; i < Arrows.Length; i++)
        {
            Arrows[i].GetComponentInChildren<TutorialIndicator>().Hide();
            Destroy(Arrows[i]);
        }
        PlaceMap();
    }

    public void nextQuestion()
    {
        if (Score > 4)
        {
            Victory();
        }
        else
        {
            StartCoroutine(NextQuestion());
            StartCoroutine(NextPhoto());
            photosound.clip = photoup;
            photosound.Play();
        }
    }

    public IEnumerator NextQuestion()
    {
        while (mapRenderer.transform.position.y < 6)
        {
            mapRenderer.transform.position += new Vector3(0, 0.1f, 0);
            yield return null;
        }
        StartCoroutine(WaitSomeTime());
    }
    IEnumerator NextQuestion2()
    {
        while (mapRenderer.transform.position.y > MapVector.y)
        {
            mapRenderer.transform.position -= new Vector3(0, 0.1f, 0);
            yield return null;
        }
        mapRenderer.transform.position = MapVector;
    }

    public IEnumerator NextPhoto()
    {
        while (photo.transform.position.y < 6.7f)
        {
            photo.transform.position += new Vector3(0, 0.1f, 0);
            yield return null;
        }
    }
    IEnumerator NextPhoto2()
    {
        if (UsingPhoto.vertical == false)
        {
            while (Vector2.Distance(photo.transform.position, PhotoVector2) >= 0.1f)
            {
                photo.transform.position = Vector2.Lerp(photo.transform.position, PhotoVector2, Time.deltaTime * 2f);
                yield return null;
            }
            photo.transform.position = PhotoVector2;
        }
        else
        {
            while (Vector2.Distance(photo.transform.position, PhotoVector1) >= 0.1f)
            {
                photo.transform.position = Vector2.Lerp(photo.transform.position, PhotoVector1, Time.deltaTime * 2f);
                yield return null;
            }
            photo.transform.position = PhotoVector1;
        }
        wait = true;
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
    IEnumerator WaitSomeTime()
    {
        yield return new WaitForSeconds(1);
        EmptyMap();
    }
}

