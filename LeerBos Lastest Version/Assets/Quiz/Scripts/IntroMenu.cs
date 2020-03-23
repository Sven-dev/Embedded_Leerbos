using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMenu : MonoBehaviour, I_SmartwallInteractable {
    [SerializeField] QuizController Controller;
    public int typeAnswers;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Hit(Vector3 hitLocation)
    {
        Controller.gameStart(typeAnswers);

    }
}
