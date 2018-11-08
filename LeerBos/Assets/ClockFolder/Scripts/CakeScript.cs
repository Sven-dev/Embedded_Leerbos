using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CakeScript : MonoBehaviour {

    //public List<CakeLayer> CakeLayers;
    public GameObject CakePrefab;

    private float yModifier;
    private float prevXScale;

    public void NextLayer()
    {
        float yPos = (transform.childCount / 1.5f) * yModifier;

        Transform newLayer = Instantiate(CakePrefab).transform;
        newLayer.SetParent(transform);

        if (transform.childCount == 1)
        {
            newLayer.localScale = new Vector2(0.2f, 0.15f);
        }
        else
        {
            newLayer.localScale = new Vector3(prevXScale * 0.85f, 0.15f);
        }
        prevXScale = newLayer.localScale.x;

        newLayer.localPosition = new Vector2(0, yPos);
        //CakeLayers.Add(layer);
    }


	// Use this for initialization
	void Start () {
        //CakeLayers = new List<CakeLayer>();
        float imgScale = CakePrefab.transform.localScale.y;
        float imgHeight = CakePrefab.GetComponent<RectTransform>().rect.height;
        yModifier = imgHeight * imgScale;
    }
}
