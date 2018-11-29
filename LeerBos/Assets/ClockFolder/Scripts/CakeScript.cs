using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CakeScript : MonoBehaviour {

    public List<Sprite> CakeSprites;
    public GameObject CakePrefab;
    public List<ArmScript> Arms;
    public bool LayersPresent;

    public List<CakeLayer> CakeLayers;
    private float yModifier;
    private float prevXScale;
    
    //put a cake layer on the counter, add it to the list
    public void NextLayer(CakeLayer layer)
    {
        float yPos = (transform.childCount / 1.5f) * yModifier;

        Transform layerTrans = layer.transform;
        layerTrans.SetParent(transform);

        //if this is the first layer
        if (transform.childCount == 1)
        {
            //scale to fixed size
            layerTrans.localScale = new Vector2(0.2f, 0.15f);
        }
        //if not
        else
        {
            //scale to relative size
            layerTrans.localScale = new Vector3(prevXScale * 0.85f, 0.15f);
        }
        prevXScale = layerTrans.localScale.x;
        layerTrans.localPosition = new Vector2(0, yPos);

        foreach(ArmScript arm in Arms)
        {
            //update the target to the current layer
            arm.ChangeTarget(layerTrans);
        }
        CakeLayers.Add(layer);
        UpdateLayersPresent();
    }

    //remove 1 slice of cake ("health") from the cake, destroy it if it was the last piece
    public void RemoveCakeSlice()
    {
        //get the most recently added layer
        CakeLayer lastLayer = CakeLayers[CakeLayers.Count - 1];
        //get its sprite
        Sprite currentSprite = lastLayer.GetComponent<Image>().sprite;
        
        //set a bool to check if we got out of the for loop due to a break or because the i ran out
        bool imageChanged = false;

        for (int i = 0; i < CakeSprites.Count - 1; i++)
        {
            Sprite cakeSprite = CakeSprites[i];
            
            //identify the current sprite
            if (currentSprite == cakeSprite)
            {
                //update the sprite of the highest layer so it has 1 less slice
                lastLayer.GetComponent<Image>().sprite = CakeSprites[i + 1];
                imageChanged = true;
                break;
            }
        }
        //make sure we didnt get out of the loop due to a break
        if (!imageChanged)
        {
            //get it out of the list and out of the game
            CakeLayers.Remove(lastLayer);
            Destroy(lastLayer.gameObject);
            UpdateLayersPresent();
            foreach (ArmScript arm in Arms)
            {
                if (LayersPresent)
                {
                    arm.ChangeTarget(CakeLayers[CakeLayers.Count - 1].transform);
                }
                //in case there's 0 layers left
                else
                {
                    //reset arms
                    arm.ResetDelay();
                    arm.ChangeTarget(transform);
                }
            }
        }
    }

    private void UpdateLayersPresent()
    {
        if (CakeLayers.Count > 0)
        {
            LayersPresent = true;
        }
        else
        {
            LayersPresent = false;
        }
    }

	// Use this for initialization
	void Start () {
        CakeLayers = new List<CakeLayer>();
        float imgScale = CakePrefab.transform.localScale.y;
        float imgHeight = CakePrefab.GetComponent<RectTransform>().rect.height;
        yModifier = imgHeight * imgScale;
    }
}
