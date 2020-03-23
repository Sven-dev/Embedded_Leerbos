using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Blink : MonoBehaviour
    {
        private bool _active;
        private Image _outline;
        public float Speed;

        // Use this for initialization
        void Start ()
        {
            _active = false;
            _outline = GetComponent<Image>();
        }

        public void StartBlink()
        {
            StartCoroutine(_Blink());
        }

        IEnumerator _Blink()
        {
            int signum = 1;
            _active = true;
            while (_active)
            {
                _outline.color = new Color(_outline.color.r, _outline.color.g, _outline.color.b, _outline.color.a + Speed * Time.deltaTime * signum);
                yield return null;

                if (_outline.color.a >= 1 || _outline.color.a <= 0)
                {
                    signum = -signum;
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }

        public void StopBlink()
        {
            StartCoroutine(_StopBlink());
        }

        IEnumerator _StopBlink()
        {
            yield return new WaitUntil(() => _outline.color.a <= 0);
            _active = false;
        }
    }
}