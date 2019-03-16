using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

//Language codes follow ISO 639-1
public class LanguageController : MonoBehaviour
{
    public static LanguageController Instance;

    public UnityEvent OnNewLanguageLoaded = new UnityEvent();
    public LanguageContainer LoadedLanguage;
    public List<string> AvailableLanguages = new List<string>();

    private void Awake()
    {
        BetterStreamingAssets.Initialize();

        Instance = this;
        //Inventorize availible laguages and make them availible
        foreach(string s in BetterStreamingAssets.GetFiles("Languages", "*.xml", SearchOption.AllDirectories))
        {
            AvailableLanguages.Add(s.Split('.')[0].Split('/')[1]);
        }
        //load english, if not availible load first langauge found
        if (LoadedLanguage == null)
        {
            if (AvailableLanguages.Contains("en"))
            {
                LoadLanguage("en");
            }
            else
            {
                LoadedLanguage = new LanguageContainer();
            }
        }
    }

    //Called by Central
    public void LoadLanguage(string languageCode)
    {
        string path = "Languages/" + languageCode + ".xml";
        if (!BetterStreamingAssets.FileExists(path))
        {
            Debug.LogErrorFormat("Streaming asset not found: {0}", path);
        }
        else
        {
            using (var stream = BetterStreamingAssets.OpenRead(path))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(LanguageContainer));
                LoadedLanguage = (LanguageContainer)serializer.Deserialize(stream);
            }
            OnNewLanguageLoaded.Invoke();
        }
    }

    public string GetText(int id)
    {
        if (id < LoadedLanguage.Texts.Count)
        {
            return LoadedLanguage.Texts[id];
        }
        return "Missing text";
    }

    public string GetImagePath(int id)
    {
        if (id < LoadedLanguage.Images.Count)
        {
            return "Languages" + LoadedLanguage.Images[id];
        }
        return "Missing text"; // missing image image
    }

    public string GetAudioFilePath(int id)
    {
        if (id < LoadedLanguage.AudioFiles.Count)
        {
            return "Languages" + LoadedLanguage.AudioFiles[id];
        }
        return "Missing text"; //missing audiofile audiofile
    }
}
