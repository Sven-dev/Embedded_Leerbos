using UnityEngine;

namespace Interval
{
    public class InteractionIndicator : MonoBehaviour
    {
        private float _minimum = .9f;
        private float _pong;
        private float speed = 8f;
        void Update()
        {
            _pong = Mathf.PingPong(Time.time / speed, 1 - _minimum);
            transform.localScale = Vector3.one * (_minimum + _pong);
        }
    }
}