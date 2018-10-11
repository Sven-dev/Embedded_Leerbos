using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleHandScript : MonoBehaviour
{
    public ScaleBeamScript BeamScript;
    public List<WeightedObjectScript> Objects;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivateWeights(WeightedObjectScript weight)
    {
        if (!Objects.Contains(weight))
        {
            print("add to list");
            Objects.Add(weight);
        }

        BeamScript.CheckWeights();
    }

    public void RemoveFromList(WeightedObjectScript weight)
    {
        Objects.Remove(weight);
        BeamScript.CheckWeights();
    }
}
