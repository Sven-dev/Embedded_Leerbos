using System.Collections;
using UnityEngine;

namespace BallMock
{
        public class MouseBall : MonoBehaviour
        {
                public GameObject Ball;
                public float Duration = 1f;
        
                void Update()
                {
                        if (Input.GetMouseButtonDown(0))
                        {
                                StartCoroutine(ShowBall());
                        }
                }

                IEnumerator ShowBall()
                {
                        var b = Instantiate(Ball);
                        b.transform.SetParent(transform, true);
                        b.transform.SetAsFirstSibling();
                        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        b.transform.position = new Vector3(pos.x, pos.y, -5f);
                        yield return new WaitForSeconds(Duration);
                        Destroy(b);
                }
        }
}