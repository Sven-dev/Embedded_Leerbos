using UnityEngine;

namespace Minigames
{
    [System.Serializable]
    public struct Answer
    {
        public Interactable Interactable;
        public bool IsCorrect;
        public Object Data;
    }
}