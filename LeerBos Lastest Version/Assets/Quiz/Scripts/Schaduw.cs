using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schaduw : MonoBehaviour {

    [SerializeField] public MainKaart controller;
    public Vector2 offset;

    private SpriteRenderer sprRndCaster;
    private SpriteRenderer sprRndShadow;

    private Transform transCaster;
    private Transform transShadow;

    public Material ShadowMaterial;
    public Color ShadowColor;
    public Color ActiveColor;
    public Color InactiveColor;
    public Color MatchColor;
	// Use this for initialization
	void Start () {
        transCaster = transform;
        transShadow = new GameObject().transform;
        transShadow.parent = transCaster;
        transShadow.gameObject.name = "shadow";

        sprRndCaster = GetComponent<SpriteRenderer>();
        sprRndShadow = transShadow.gameObject.AddComponent<SpriteRenderer>();

        sprRndShadow.material = ShadowMaterial;
        //sprRndShadow.color = ShadowColor;
        
        sprRndShadow.sortingLayerName = sprRndCaster.sortingLayerName;
        //sprRndShadow.sortingOrder = sprRndCaster.sortingOrder - 1;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transShadow.position = new Vector2(transCaster.position.x + offset.x, transCaster.position.y + offset.y);
        transShadow.localScale = new Vector3(1.0f,1f,1);
        sprRndShadow.sprite = sprRndCaster.sprite;
        sprRndShadow.color = ShadowColor;
    }
}
