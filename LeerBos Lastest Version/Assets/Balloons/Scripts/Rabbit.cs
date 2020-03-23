using System;
using System.Collections;
using System.Collections.Generic;
using Balloons.Scripts;
using UnityEngine;

public class Rabbit : MonoBehaviour
{

	/***
	 * This is done this way because both animation sprites have a slight discontinuity.
	 * This way they have been positioned within their parent - this gameObject.
	 */
	public GameObject Throw;
	public GameObject Stand;
	public BalloonStack Stack;
	public 
	
	void Awake()
	{
		DoStand();
	}

	public void Animate(float seconds)
	{
		StopAllCoroutines();
		StartCoroutine(SpriteSwap(seconds));
	}

	private IEnumerator SpriteSwap(float seconds)
	{
		DoThrow();
		yield return new WaitForSeconds(seconds);
		DoStand();
	}

	private void DoStand()
	{
		Stand.SetActive(true);
		Throw.SetActive(false);
	}

	private void DoThrow()
	{
		Throw.SetActive(true);	
		Stand.SetActive(false);
	}
}
