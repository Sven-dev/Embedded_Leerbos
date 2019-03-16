using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageContainer
{
    public List<string> Texts;

    public List<string> Images;

    public List<string> AudioFiles;

    public LanguageContainer()
    {
        Texts = new List<string>();
        Images = new List<string>();
        AudioFiles = new List<string>();
    }
}
