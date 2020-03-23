using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTimer : SceneSwitchable
{
	// Use this for initialization
	void Start ()
    {
        GlobalVariables.Reset();
        StartCoroutine(_Wait());
	}
	
    IEnumerator _Wait()
    {
        yield return new WaitForSeconds(20);
        Switch();
    }
}