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
		gameState = GameState.Idle;
	    objectsWeighed = 0;
        CheckGameState();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public void CheckGameState()
    {
        switch (gameState)
        {
                case GameState.Idle:
                    //if you've successfully weighed 5 things
                    if (objectsWeighed >= 5 || ObjectsToWeigh.Count == 0)
                    {
                        gameState = GameState.Victory;
                        CheckGameState();
                    }
                    else
                    {
                        SetGameState(GameState.Resetting);
                        ResetGame();
                }
                    break;
                case GameState.Playing:
                    //if the scales are equal
                    if (LeftHand.GetTotalMass() == RightHand.GetTotalMass())
                    {
                        objectsWeighed++;
                        SetGameState(GameState.Idle);
                        CheckGameState();
                    }
                    break;
                case GameState.Victory:
                    gameState = GameState.GameOver;
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
            foreach (WeightedObjectScript weight in WeightParent.GetComponentsInChildren<WeightedObjectScript>())
            {
                weight.SelfDestruct();
            }
        }
        CurrentObject = ObjectsToWeigh[Random.Range(0, ObjectsToWeigh.Count)];
        CurrentObject = SpawnPrefab(CurrentObject,ScaleHand.Right);
        ObjectsToWeigh.Remove(CurrentObject);
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

public enum GameState
{
    Idle,
    Resetting,
    Playing,
    Victory,
    GameOver
}

public enum ScaleHand
{
    Left,
    Right
}
