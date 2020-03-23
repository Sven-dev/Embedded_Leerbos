using System.Collections;
using UnityEngine;

namespace Balloons
{
    public class Line : MonoBehaviour
    {
        public Vector3 Origin;
        public Vector3 Target;
        public Balloon ConnectedTo;
        public bool Moving = true;
        
        void Start()
        {
            transform.localScale = new Vector3(0, 1, 1);         
            transform.position = Origin;
            var angle = Mathf.Atan2(Target.y-Origin.y, Target.x-Origin.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            StartCoroutine(Shoot(new Vector3(Vector3.Distance(Origin, Target) / 2f, 1, -1), Target));
        }

        private void Update()
        {
            if (ConnectedTo &! _shooting)
            {
                Move(ConnectedTo.Pivot.position);
            }
        }

        private void Move(Vector3 target)
        {
            transform.position = Vector3.Lerp(Origin, target, 0.5f);
            var angle = Mathf.Atan2(target.y-Origin.y, target.x-Origin.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            transform.localScale = new Vector3(Vector3.Distance(Origin, target) / 2f, 1, 1);
        }

        private bool _shooting = true;
        private IEnumerator Shoot(Vector3 targetScale, Vector3 targetPos)
        {
            _shooting = true;
            var time = 0f;
            var halfPos = Vector3.Lerp(Origin, targetPos, 0.5f);
            while (transform.localScale != targetScale)
            {
                time += Time.deltaTime;
                transform.localScale = Vector3.Lerp(transform.localScale, targetScale, time);
                transform.position = Vector3.Lerp(transform.position, halfPos, time);
                yield return null;
            }
            Moving = false;
            if(ConnectedTo == null) {
                time = 0f;
                var defaultScale = new Vector3(0, 1, 1);
                while (transform.localScale != defaultScale)
                {
                    time += Time.deltaTime;
                    transform.localScale = Vector3.Lerp(transform.localScale, defaultScale, time);
                    transform.position = Vector3.Lerp(transform.position, targetPos, time);
                    yield return null;
                }
                Destroy(gameObject);
            }
            else
            {
                _shooting = false;
            }
        }

    }
}