using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingApple : MonoBehaviour
{
    public PawScript paw;
    // Use this for initialization
    void FallDownEvent()
    {
        paw.AnimAppleStart = false;
    }
}
