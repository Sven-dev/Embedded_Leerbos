using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bands : MonoBehaviour
{

    public List<GameObject> Karts = new List<GameObject>();
    public bool BandsMoving = false;
    public int i = 0;
    [SerializeField] private Karts TrainScript;
    [SerializeField] private Audio AudioClips;
    float[] kartsloc = new float[] { -7, -5.1f, -3.1f, -1.1f, 1.1f, 3.2f, 5.5f };
    // Use this for initialization
    public void CountTrains()
    {
        Karts.Clear();
        foreach (GameObject KartText in GameObject.FindGameObjectsWithTag("KartTrain"))
        {
            if (KartText != null)
            {
                Karts.Add(KartText);
            }
            Debug.Log(Karts);
        }
        StartCoroutine(findcards());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator moveBand(Vector2 Pos)
    {
        BandsMoving = false;
        AudioClips.Source.clip = AudioClips.Bands;
        AudioClips.Source.Play();
        while (Vector2.Distance(transform.position, Pos) >= 0.05f)
        {
            transform.position = Vector2.Lerp(transform.position, Pos, Time.deltaTime * 4f);
            yield return null;
        }
        AudioClips.Source.Stop();
        BandsMoving = true;
    }
    IEnumerator findcards()
    {
        yield return new WaitForSeconds(0.2f);
        findKarts();
    }
    public void findKarts()
    {
        for (var i = Karts.Count - 1; i > -1; i--)
        {
            if (Karts[i] == null)
                Karts.RemoveAt(i);
            Debug.Log("Help");
        }
        for (int i = 0; i < Karts.Count; i++)
        {
            if (Karts[i].GetComponent<Text>().text == "...")
            {
                StartCoroutine(moveBand(new Vector2(kartsloc[i], transform.position.y)));
                Debug.Log(new Vector2(kartsloc[i], transform.position.y));
                //Karts.Remove(Karts[0]);
                //return;
                break;
            }
            else if(Karts[i] == null)
            {
                Karts.Remove(Karts[i]);
                i--;
            }
        }
        //if (Vector2.Distance(TrainScript.Train.transform.position, TrainScript.targetMiddle.position) <= 0.01f && DontGo == false)
        //{
        //    StartCoroutine(TrainScript.TrainWait());
        //}
    }
    public void RemoveAll()
    {
        if (Vector2.Distance(TrainScript.Train.transform.position, TrainScript.targetMiddle.position) <= 0.01f)// && DontGo == false)
        {
            StartCoroutine(TrainScript.TrainWait());
        }
    }
}
