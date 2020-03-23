using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLevelChange : MonoBehaviour {

    [SerializeField] private GameObject targetObject;
    [SerializeField] private string targetMessage2;
    // Use this for initialization
    public void OnMouseDown()
    {

    }

    // Update is called once per frame
    public void OnMouseUp()
    {
        if (targetObject != null)
        {
            targetObject.SendMessage(targetMessage2);
        }
    }
}
