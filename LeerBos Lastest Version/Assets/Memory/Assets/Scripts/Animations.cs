using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Animations : MonoBehaviour
{
    private void Start()//Zet kaarten op spel
    {
        StartCoroutine(Disappear());
    }
    public IEnumerator Disappear()
    {
        yield return new WaitForSeconds(0.75f);
        DestroyImmediate(this);
        yield return null;
    }
}