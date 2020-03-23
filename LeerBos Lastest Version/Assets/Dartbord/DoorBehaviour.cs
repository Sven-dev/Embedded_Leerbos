using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBehaviour : MonoBehaviour {
    public GameRules controller;
    public GameRulesV2 controllerV2;
    public GameObject[] Numbers;
    public GameObject LeftDoor;
    public GameObject RightDoor;
    public GameObject Lock;
    private int Score;
    private GameObject Item;
    public Animator Camera;

    public SoundDart AudioManager;

    // Use this for initialization
    void Start() {

    }


    public void LockBehavoir()
    {
        if (SceneManager.GetActiveScene().name == "DartenV2")
        {
            Score = controllerV2.Score;
            Item = Numbers[Score];
            StartCoroutine(LockOpenAnim());
        }
        else
        {
            Score = controller.Score;
            Item = Numbers[Score];
            StartCoroutine(LockOpenAnim());
        }
    }


    public IEnumerator LockOpenAnim()
    {
        AudioManager.LockOpenSound();
        if (Score == 0 || Score == 3)
        {
            for (int i = 0; i < 50; i++)
            {
                Item.transform.Rotate(Vector3.right, -1);
                yield return null;
            }
            if(Score == 3)
            {
                Score++;
            }
        }
        else if (Score == 1)
        {
            for (int i = 0; i < 50; i++)
            {
                Item.transform.Rotate(Vector3.left, -1);
                yield return null;
            }
        }
        else if (Score == 2)
        {
            for (int i = 0; i < 80; i++)
            {
                Item.transform.Rotate(Vector3.left, -1);
                yield return null;
            }
        }
        if (Score == 4)
        {
            LockOpen();
        }
    }
    private void LockOpen()
    {
        StartCoroutine(_LockOpen());
    }
    public IEnumerator _LockOpen()
    {
        Debug.Log("LockStart");
        AudioManager.OpenSound();
        while (Lock.transform.position.x <= 2.95f)
        {
            Lock.transform.transform.Translate(-0.035f, 0, 0);
            Debug.Log("Suppoed End");
            yield return null;
        }
        DoorOpen();
    }
    private void DoorOpen()
    {
        StartCoroutine(_DoorOpen());
        Camera.SetBool("Finish", true);
        Victory();
    }
    public IEnumerator _DoorOpen()
    {
        Debug.Log("DoorStart");
        for (int i = 0; i < 60; i++)
        {
            LeftDoor.transform.Rotate(Vector3.up, 1f);
            RightDoor.transform.Rotate(Vector3.down, 1f);
            Debug.Log("Real End");
            yield return null;
        }
    }

    public VictoryScript VictoryCanvas;
    private void Victory()
    {
        StartCoroutine(_Victory());
    }

    IEnumerator _Victory()
    {
        yield return new WaitForSeconds(0.5f);
        VictoryCanvas.Enable();
    }
}
