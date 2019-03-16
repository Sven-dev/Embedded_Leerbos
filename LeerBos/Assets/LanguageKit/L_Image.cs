using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class L_Image : MonoBehaviour
{
    public int ID;
    public Image Img;

    // Start is called before the first frame update
    void Start()
    {
        LanguageController.Instance.OnNewLanguageLoaded.AddListener(Load);
        Load();
    }

    void Load()
    {
        //Img.sprite = Sprite.Create((Texture2D)AssetDatabase.LoadAssetAtPath(LanguageController.Instance.GetImagePath(ID), typeof(Texture2D)), Img.sprite.rect, new Vector2(0.5f,0.5f), Img.sprite.pixelsPerUnit);
    }
}
