using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class L_Audio : MonoBehaviour
{
    public int ID;
    public AudioSource Source;

    // Start is called before the first frame update
    void Start()
    {
        LanguageController.Instance.OnNewLanguageLoaded.AddListener(Load);
        Load();
    }

    void Load()
    {
        //Source.clip = (AudioClip)AssetDatabase.LoadAssetAtPath(LanguageController.Instance.GetAudioFilePath(ID), typeof(AudioClip));
    }
}
