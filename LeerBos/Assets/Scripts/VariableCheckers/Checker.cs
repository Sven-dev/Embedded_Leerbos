using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Checker : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(_Start());
    }

    IEnumerator _Start()
    {
        yield return new WaitForSeconds(1.5f);
        Check();
    }

    protected abstract void Check();
}