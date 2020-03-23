using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DartBordRoll : MonoBehaviour {
    private float speed;
    private float slowdown;
    public GameRules controller;
    public GameRulesV2 controllerV2;
    public GameObject DartBord;
	// Use this for initialization
	void Start () {
		
	}
    private void OnMouseDown()
    {
        
    }

    // Update is called once per frame
    void Update () {
		
	}
    public IEnumerator Rotation(float Direction)
    {
        if (SceneManager.GetActiveScene().name == "DartenV2")
        {
            speed = Random.Range(controllerV2.Score - 0.5f, controllerV2.Score + 0.5f);
        }
        else
        {
            speed = Random.Range(controller.Score - 0.5f, controller.Score + 0.5f);
        }
        slowdown = 0.03f; 
        while (speed > 0)
        {
            if (Direction < 0)
            {
                DartBord.transform.Rotate(Vector3.back, speed);
            }
            else if (Direction > 0)
            {
                DartBord.transform.Rotate(Vector3.forward, speed);
            }
            speed = speed - slowdown;
            yield return null;
        }
    }
}
