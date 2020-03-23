using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResetManager : MonoBehaviour, I_SmartwallInteractable
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private string targetMessage;
	// Use this for initialization
	public void OnMouseDown () {
		
	}

    // Update is called once per frame
    public void Hit(Vector3 clickposition)
    {
        if (targetObject != null)
        {
            targetObject.SendMessage(targetMessage);
        }
    }
}
