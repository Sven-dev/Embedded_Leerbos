using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneChecker : Checker
{
    public Saveable IntroState;
    public Saveable CompleteState;
    [Space]
    public Npc Character;
    public List<GameObject> Decorations;

    protected override void Check()
    {
        if (IntroState.Get() == false)
        {
            IntroState.Set(true);
            StartCoroutine(_PlayDialogue());
        }
        else if (CompleteState.Get() == true)
        {
            foreach (GameObject decoration in Decorations)
            {
                decoration.SetActive(true);
            }

            Character.PlayDialogue(Character.VictoryClips[0]);
        }
    }

    IEnumerator _PlayDialogue()
    {
        Character.PlayDialogue(Character.IntroClips);
        while (Character.DialoguePlaying)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        Character.ActivateBlinkables();
    }
}