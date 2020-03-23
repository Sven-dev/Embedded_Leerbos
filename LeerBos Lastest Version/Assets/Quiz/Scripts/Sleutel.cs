using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleutel : MonoBehaviour {
    [SerializeField] public GameObject _Sleutel;
    [SerializeField] public QuizController controller;
    [SerializeField] public GameObject Question;
    [SerializeField] public GameObject Answer;
    private Rigidbody2D Zwaartekracht;
    // Use this for initialization
    void Start () {
        Zwaartekracht = _Sleutel.GetComponent<Rigidbody2D>();
        Zwaartekracht.isKinematic = true;
    }

    // Update is called once per frame
    void Update () {
		if (controller._score == 4) 
        {
            Zwaartekracht.isKinematic = false;
            Question.SetActive(false);
            Answer.SetActive(false);
}
	}
}
