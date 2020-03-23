using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour, I_SmartwallInteractable {
    public int Modus;
    [SerializeField] ControllerDolhof controller;
    [SerializeField] Canvas _Menu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Hit(Vector3 hitposition)
    {
        controller.Install(Modus);
        //_Menu.enabled = false;
        int i = 0;
        GameObject[] allChildren = new GameObject[_Menu.transform.childCount];

        foreach (Transform child in _Menu.transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        //Now destroy them
        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child.gameObject);
        }
    }
}
