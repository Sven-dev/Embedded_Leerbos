using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardFieldV2 : MonoBehaviour, I_SmartwallInteractable
{
    [SerializeField] public int Value;
    [SerializeField] public GameRulesV2 controller;
    [SerializeField] DartBordRoll Rotation;
    [SerializeField] SoundDart HitSound;
    [SerializeField] public MinPlusScript MinPlus;

    public void Hit(Vector3 clickposition)
    {
            if (MinPlus.Min == false)
            {
                controller.Cijfers.Add(Value);
                controller.SumCount(Value);
                StartCoroutine(Rotation.Rotation(transform.position.x));
                HitSound.SoundPlay();
            }
            else if (MinPlus.Min == true)
            {
                controller.Cijfers.Add(0 - Value);
                controller.SumCount(Value);
                StartCoroutine(Rotation.Rotation(transform.position.x));
                HitSound.SoundPlay();
            }
        
    }
}