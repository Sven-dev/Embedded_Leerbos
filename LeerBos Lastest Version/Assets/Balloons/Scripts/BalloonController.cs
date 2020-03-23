using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Balloons.Scripts;
using UnityEngine;

namespace Balloons
{
    public class BalloonController : MonoBehaviour
    {
        [Header("Spawn")] public Transform Origin;
        public BalloonPositions Positions;
        public Transform Container;
        public Color[] ColorPalette;
        public GameObject BalloonPrefab;
        public Interactable Interactable;
        public LineShooter LineShooter;
        public BalloonStack Stack;
                
        private void Awake()
        {
            Screen.SetResolution(1600, 1200, true);
            Interactable = GetComponentInChildren<Interactable>();
            if (Interactable == null) return;
            Interactable.OnClick.AddListener(() => LineShooter.MakeLine(Interactable._hitPosition));
        }

        public Balloon[] SpawnBalloons(int amount)
        {
            if (Positions.Positions.Length < amount) amount = Positions.Positions.Length;
            var ret = new Balloon[amount];
            for (int i = 0; i < amount; i++)
            {
                var balloon = ret[i] = Instantiate(BalloonPrefab, Container).GetComponent<Balloon>();
                balloon.GetComponentInChildren<SpriteRenderer>().color = ColorPalette[i % ColorPalette.Length];
                balloon.OnHit.AddListener(() => StartCoroutine(HitBalloon(balloon)));
                balloon.transform.position = Positions.Positions[i].position;
                balloon.SpawnPoint = Origin;
            }
            return ret;
        }

        IEnumerator HitBalloon(Balloon b, Line line = null)
        {
            if(!line)line = LineShooter.ShootLine(b.Pivot.position);
            line.ConnectedTo = b;
            b.Line = line;
            b.transform.parent = Stack.transform;
            yield return new WaitForSeconds(LineShooter.AnimationDuration);
            line.Origin = Stack.StackOrigin.position;
            var p = Stack.NextSlot();
            line.transform.parent = p;
            yield return b.Move(p.position);
            b.StartFloat();
        }

        public void Log()
        {
            Debug.Log("Clicked");
        }

        public IEnumerator PopBalloons()
        {
            var b = GetComponentsInChildren<Balloon>();
            foreach (Balloon bal in new List<Balloon>(b))
            {
                bal.Pop();
                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            }
        }

        public void PopRandomBalloon()
        {
            var b = GetComponentsInChildren<Balloon>();
            b[Random.Range(0, b.Length)].Pop();
        }
    }
}