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

    private GameObject CurrentObject;
    private GameState gameState;
    private int objectsWeighed;

	// Use this for initialization
	void Start () {
		gameState = GameState.Resetting;
	    objectsWeighed = 0;
        CheckGameState();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckGameState()
    {
        switch (gameState)
        {
                case GameState.Resetting:
                    //if you've successfully weighed 5 things
                    if (objectsWeighed >= 5)
                    {
                        gameState = GameState.Victory;
                        CheckGameState();
                    }
                    ResetGame();
                    gameState = GameState.Playing;
                    break;
                case GameState.Playing:
                    //if the scales are equal
                    if (LeftHand.GetTotalMass() == RightHand.GetTotalMass())
                    {
                        objectsWeighed++;
                        gameState = GameState.Resetting;
                        CheckGameState();
                    }
                    else
                    {
                        //wrong answer feedback
                        print("wrong");
                    }
                    break;
                case GameState.Victory:
                    print("A WINNER IS YOU");
                    break;
        }
    }

    public void ResetGame()
    {
        //if not first rotation
        if (CurrentObject != null)
        {
            RightHand.Objects.Remove(CurrentObject.GetComponent<WeightedObjectScript>());
            Destroy(CurrentObject);
        }
        CurrentObject = ObjectsToWeigh[Random.Range(0, ObjectsToWeigh.Count)];
        ObjectsToWeigh.Remove(CurrentObject);
        SpawnPrefab(CurrentObject,ScaleHand.Right);
    }

    //spawn the submitted prefab above one of the scales
    public void SpawnPrefab(GameObject prefab,ScaleHand hand)
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
        Instantiate(prefab, new Vector2(x, 10),Quaternion.Euler(0,0,0),WeightParent.transform);
    }

    //get x position of the left scale
    float GetHandX(ScaleHandScript hand)
    {
        return hand.gameObject.transform.position.x;
    }
}

public enum GameState
{
    Resetting,
    Playing,
    Victory
}

public enum ScaleHand
{
    Left,
    Right
}
