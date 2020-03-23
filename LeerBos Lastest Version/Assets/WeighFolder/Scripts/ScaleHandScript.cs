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

    //collect the mass of every object on the scale
    public int GetTotalMass()
    {
        int mass = 0;

        foreach (WeightedObjectScript weight in Objects)
        {
            mass += weight.Mass;
        }

        return mass;
    }

    //add weight to calculation if it isn't in it already, and calculate
    public void ActivateWeights(WeightedObjectScript weight)
    {
        if (!Objects.Contains(weight))
        {
            Objects.Add(weight);
        }

        BeamScript.CheckWeights();
    }

    //remove weight from calculation and calculate
    public void RemoveFromList(WeightedObjectScript weight)
    {
        Objects.Remove(weight);
        BeamScript.CheckWeights();
    }
}
