using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressChecker : MonoBehaviour
{
    [Space]
    public List<Saveable> Minigames;
    [Space]
    public Saveable ZoneComplete;
    public List<Sprite> Decorations;
    private Image Decoration;

    private void Start()
    {
        Decoration = GetComponent<Image>();
        bool Completed = true;
        for (int i = 0; i < Minigames.Count; i++)
        {
            if (Minigames[i].Get() == false)
            {
                Completed = false;
            }
            else
            {
                Decoration.sprite = Decorations[i];
            }
        }

        if (Completed)
        {
            ZoneComplete.Set(true);
            Decoration.sprite = Decorations[Decorations.Count-1];
        }
    }
}