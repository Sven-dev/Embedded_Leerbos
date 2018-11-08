using UnityEngine;

[System.Serializable]
public abstract class Saveable : MonoBehaviour
{
    public abstract bool Get();
    public abstract void Set(bool value);
}