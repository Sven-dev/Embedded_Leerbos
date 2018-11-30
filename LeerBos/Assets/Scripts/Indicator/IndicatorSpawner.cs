using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorSpawner : MonoBehaviour
{
    public TutorialIndicator Prefab;
    public AfkTimer AfkTimer;

	// Use this for initialization
	void Start ()
    {
        //PrefabSpawnButtonScript.OnWeightSpawn += SpawnIndicator;
	}
	
    protected void SpawnIndicator(GameObject target)
    {
        TutorialIndicator i = Instantiate(Prefab);
        i.Target = target;
        AfkTimer.Indicators.Add(i);
    }

    private void OnDestroy()
    {
        //PrefabSpawnButtonScript.OnWeightSpawn -= SpawnIndicator;
    }
}