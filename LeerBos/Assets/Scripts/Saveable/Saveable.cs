using UnityEngine;

[System.Serializable]
public abstract class Saveable : MonoBehaviour
{
    public abstract int Value
    {
        get;
        set;
    }
}