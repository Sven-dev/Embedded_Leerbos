using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Balloons
{
    public class LineShooter : MonoBehaviour
    {
        public Line LinePrefab;
        public float AnimationDuration;
        public Rabbit Rabbit;

        public Line ShootLine(Vector3 position)
        {
            var line = Instantiate(LinePrefab);
            line.Origin = transform.position;
            line.Target = position;
            Rabbit.Animate(AnimationDuration);
            return line;
        }

        public void MakeLine(Vector3 position)
        { 
        int objects = 0;
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Line"))
            {
                if (obj.GetComponent<Line>().Moving == true)
                {
                    objects++;
                }
            }
            if (objects == 0)
            {
                ShootLine(position);
            }
        }
    }
}