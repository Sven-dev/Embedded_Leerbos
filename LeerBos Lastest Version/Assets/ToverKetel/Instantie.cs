using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instantie : MonoBehaviour
{
    public int Score = 0;
    public float Scale = 0;
    private static Instantie Referentie;
    // Use this for initialization
    void Start()
    {
        if (Referentie == null)
        {
            Referentie = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
       if( Score == 5)
        {
            Victory();
        }
    }

    private void Victory()
    {
        StartCoroutine(_Victory());
    }

    IEnumerator _Victory()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
