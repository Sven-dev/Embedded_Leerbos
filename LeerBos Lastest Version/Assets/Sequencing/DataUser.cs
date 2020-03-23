using Minigames;
using UnityEngine;
//using UnityScript.Lang;
using Array = System.Array;

namespace Sequencing
{
    public class DataUser : MonoBehaviour
    {
        [SerializeField]
        public object Current;
        public Object[] Data;

        public void SetRound(int i)
        {
            Current = Data.Length > i ? Data[i] : Data[0];
        }

        public T ReturnCurrentData<T>()
        {
            return (T) Current;
        }
    }
}