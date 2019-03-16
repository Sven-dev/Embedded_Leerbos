using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_TextMesh : MonoBehaviour
{
    public int ID;
    public TextMesh Label;

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
