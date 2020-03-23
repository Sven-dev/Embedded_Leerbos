using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class BoardField  : MonoBehaviour, I_SmartwallInteractable
{
    [SerializeField] public int Value;
    [SerializeField] public GameRules controller;
    [SerializeField] public GameRulesV2 controllerV2;
    [SerializeField] DartBordRoll Rotation;
    [SerializeField] SoundDart HitSound;
    [SerializeField] public MinPlusScript MinPlus;

    public void Hit(Vector3 clickposition)
    {
        if (SceneManager.GetActiveScene().name == "DartenV2")
        {
            if (MinPlus.Min == false)
            {
                controllerV2.Cijfers.Add(Value);
                controllerV2.SumCount(Value);
                StartCoroutine(Rotation.Rotation(transform.position.x));
                HitSound.SoundPlay();
            }
            else if (MinPlus.Min == true)
            {
                controllerV2.CijfersMin.Add(Value);
                controllerV2.SumCount(Value);
                StartCoroutine(Rotation.Rotation(transform.position.x));
                HitSound.SoundPlay();
            }
        }
        else
        {
            if (MinPlus.Min == false)
            {
                controller.Cijfers.Add(Value);
                controller.SumCount();
                StartCoroutine(Rotation.Rotation(transform.position.x));
                HitSound.SoundPlay();
            }
            else if (MinPlus.Min == true)
            {
                controller.Cijfers.Add(0 - Value);
                controller.SumCount();
                StartCoroutine(Rotation.Rotation(transform.position.x));
                HitSound.SoundPlay();
            }
        }
    }
}