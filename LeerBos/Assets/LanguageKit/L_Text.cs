using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L_Text : MonoBehaviour
{
    public int ID;
    public Text Label;

    // Start is called before the first frame update
    void Start()
    {
        LanguageController.Instance.OnNewLanguageLoaded.AddListener(Load);
        Load();
    }

    void Load()
    {
        Label.text = LanguageController.Instance.GetText(ID);
    }
}
