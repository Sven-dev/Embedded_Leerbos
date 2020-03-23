using UnityEngine;

namespace Minigames
{
    public class Saveable : MonoBehaviour
    {
        public string Key;
        private SaveState _state;

        void OnEnable()
        {
            if (PlayerPrefs.HasKey(Key))
            {
                _state = (SaveState)PlayerPrefs.GetInt(Key);
            }
            else
            {
                _state = SaveState.Default;
            }
        }

        public void SetState(SaveState state)
        {
            _state = State;
            PlayerPrefs.SetInt(Key, (int)state);
        }
        
        public SaveState State
        {
            get { return _state; }
        }

        public void Reset()
        {
            _state = SaveState.Default;
            PlayerPrefs.DeleteKey(Key);
        }
    }

    [System.Serializable]
    public enum SaveState
    {
        Default, Introduced, Completed, IconInCart
    }
}