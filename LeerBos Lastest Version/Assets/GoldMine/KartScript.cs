using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KartScript : MonoBehaviour {
    [SerializeField]private Karts SentenceScript;
    public Words Word;
    [SerializeField] private Audio AudioClips;
    [SerializeField] private Bands Bands;
    [SerializeField] private Gold GoldBLockSpawner;
    [SerializeField] private Karts KartsScript;
    bool able = true;
    public int VictoryPoints = 0;
    // Use this for initialization

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.name == "Gold(Clone)" && transform.parent.name == "Kart(Clone)" && able == true)
        {
            if (col.gameObject.GetComponentInChildren<Text>().text == Word.Word)
            {
                AudioClips.Source.clip = AudioClips.GoodAnswer;
                AudioClips.Source.Play();
                AudioClips.SecondSource.clip = AudioClips.OnGround;
                AudioClips.SecondSource.Play();
                Bands.RemoveAll();
                able = false;
                col.gameObject.GetComponent<Rigidbody2D>().simulated = false;
                KartsScript.VictoryPoints++;
                if (KartsScript.VictoryPoints == 4)
                {
                    Victory();
                }
            }
            else if (Word.Justwords.Length == 1 && col.gameObject.GetComponentInChildren<Text>().text == Word.Justwords[0])
            {
                AudioClips.Source.clip = AudioClips.GoodAnswer;
                AudioClips.Source.Play();
                AudioClips.SecondSource.clip = AudioClips.OnGround;
                AudioClips.SecondSource.Play();
                Bands.RemoveAll();
                able = false;
                col.gameObject.GetComponent<Rigidbody2D>().simulated = false;
                KartsScript.VictoryPoints++;
                if (KartsScript.VictoryPoints == 4)
                {
                    Victory();
                }
            }
            else if (Word.Justwords.Length == 2 && col.gameObject.GetComponentInChildren<Text>().text == Word.Justwords[1])
            {
                AudioClips.Source.clip = AudioClips.GoodAnswer;
                AudioClips.Source.Play();
                AudioClips.SecondSource.clip = AudioClips.OnGround;
                AudioClips.SecondSource.Play();
                Bands.RemoveAll();
                able = false;
                col.gameObject.GetComponent<Rigidbody2D>().simulated = false;
                KartsScript.VictoryPoints++;
                if (KartsScript.VictoryPoints == 4)
                {
                    Victory();
                }
            }
            else
            {
                AudioClips.Source.clip = AudioClips.BadAnswer;
                AudioClips.Source.Play();
                GameObject p = col.gameObject;
                Destroy(p.GetComponent<Collider2D>());
                p.transform.Translate(Vector3.up);
                Rigidbody2D rigidbody = p.GetComponent<Rigidbody2D>();
                rigidbody.AddForce((Vector2.up * 2 + Vector2.left) * 125);
                rigidbody.AddTorque(35);
                rigidbody.gravityScale = 1;
            }
            GoldBLockSpawner.SpawnGold(0);
        }
    }
    [SerializeField] private VictoryScript VictoryCanvas;
    private void Victory()
    {
        StartCoroutine(_Victory());
    }

    IEnumerator _Victory()
    {
        yield return new WaitForSeconds(0.5f);
        VictoryCanvas.Enable();
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.9f);
        able = true;
    }
}
