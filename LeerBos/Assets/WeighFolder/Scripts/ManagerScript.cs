using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ManagerScript : MonoBehaviour
{
    public GameObject WeightParent;
    public ScaleHandScript LeftHand;
    public ScaleHandScript RightHand;
    public List<GameObject> ObjectsToWeigh;
    public VictoryScript VictoryLabel;
    public ProgressBarScript ProgressBar;
    public float CountDownTime;
    
    private GameObject currentObject;
    private bool gameOver;
    private int objectsWeighed;
    private string currentMassString;
    private int coroutineId;
    private bool coroutineRunning;

	// Use this for initialization
	void Start ()
	{
	    gameOver = false;
	    objectsWeighed = 0;
	    coroutineId = 0;
        ResetGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckAnswer()
    {
        if (gameOver == false)
        {
            int leftMass = LeftHand.GetTotalMass();
            int rightMass = RightHand.GetTotalMass();

            string massString = leftMass.ToString() + rightMass.ToString();
            if (currentMassString != massString)
            {
                currentMassString = massString;
                if (leftMass == rightMass && leftMass + rightMass > 0)
                {
                    coroutineId++;
                    StartCoroutine(_CheckAnswer(currentMassString, coroutineId));
                }
            }
        }
    }

    IEnumerator _CheckAnswer(string submittedMassString,int id)
    {
        ProgressBar.gameObject.SetActive(true);

        float animationTime = CountDownTime;
        while (submittedMassString == currentMassString && animationTime > 0)
        {
            animationTime -= Time.deltaTime;
            ProgressBar.SetProgress(1 - (animationTime / CountDownTime));
            yield return null;
        }
        //cancel if a new check was initiated
        if (coroutineId == id)
        {
            ProgressBar.gameObject.SetActive(false);
            if (submittedMassString == currentMassString)
            {
                CorrectAnswer();
            }
        }
    }

    public void CorrectAnswer()
    {
        objectsWeighed++;
        

        if (objectsWeighed >= 5)
        {
            gameOver = true;
            print("A WINNER IS YOU");
            VictoryLabel.Enable();
        }
        else
        {
            ResetGame();
        }
    }

    

    public void ResetGame()
    {
        //if not first rotation
        if (currentObject != null)
        {
            foreach (WeightedObjectScript weight in WeightParent.GetComponentsInChildren<WeightedObjectScript>())
            {
                weight.SelfDestruct();
            }
        }
        int rnd = Random.Range(0, ObjectsToWeigh.Count);
        print(rnd);
        currentObject = ObjectsToWeigh[rnd];
        print(currentObject);
        ObjectsToWeigh.Remove(currentObject);
        currentObject = SpawnPrefab(currentObject,ScaleHand.Right);
        

    }

    //spawn the submitted prefab above one of the scales
    public GameObject SpawnPrefab(GameObject prefab,ScaleHand hand)
    {
        float x;
        switch (hand)
        {
            case ScaleHand.Left:
                x = GetHandX(LeftHand);
                break;
            case ScaleHand.Right:
                x = GetHandX(RightHand);
                break;
            default:
                print("how the heck did you get here with an invalid enum");
                throw new ArgumentException();
        }
        return Instantiate(prefab, new Vector2(x, 10),Quaternion.Euler(0,0,0),WeightParent.transform);
    }

    //get x position of the left scale
    float GetHandX(ScaleHandScript hand)
    {
        return hand.gameObject.transform.position.x;
    }
}

public enum ScaleHand
{
    Left,
    Right
}
