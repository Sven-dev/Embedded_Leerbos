using System.Collections;
using Balloons.Scripts;
using UnityEngine;
using UnityEngine.Events;

namespace Balloons
{
	public class Balloon : MonoBehaviour, I_SmartwallInteractable
	{
		public UnityEvent OnHit;

		private Vector3 _startPos;
		private Vector3 _initPos;
		private Vector3 _splitPos;
		public Transform SpawnPoint;
		public Transform Pivot;
		public float MinimalStraightY;
		public float PffffftDuration = 1f;
		public Line Line;

        private Animator _anim;

		private bool _moveIn8;

		public float Speed = 1;
		public float XScale = 1;
		public float YScale = 1;

		void Start ()
		{
			_anim = GetComponentInChildren<Animator>();
			Speed += Random.Range(Speed / -20, Speed / 20);
			XScale *= Random.Range(0, 2) > 0 ? 1 : -1;
			_startPos = transform.position;
			transform.position = _initPos = SpawnPoint.position + new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 1);
			_splitPos = _initPos + Vector3.up * MinimalStraightY;
			StartCoroutine(MoveToStart());
		}

		IEnumerator MoveToStart()
		{
			var angle = Mathf.Atan2(_startPos.y-_initPos.y, _startPos.x-_initPos.x) * Mathf.Rad2Deg;
			yield return MoveTowards(_initPos, _splitPos, 2f, Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(0, 0, 270 + angle), 0.5f));
			yield return MoveTowards(_splitPos, _startPos, 5f, Quaternion.Euler(0, 0, 270 + angle));
			_8pos = transform.position;
			_moveIn8 = true;
		}

		public void Hit(Vector3 hitPosition)
		{
			StopAllCoroutines();
			if (GetComponentInChildren<BalloonUi>().IsCorrect)
			{
				OnHit.Invoke();
				_moveIn8 = false;
			}
			else
			{
				_moveIn8 = true;
				StartCoroutine(Pffffft());
			}
		}

		private float _speedMod = 1f;

        [SerializeField] AudioClip PffftSound;
		IEnumerator Pffffft()
		{
            GetComponent<AudioSource>().clip = PffftSound;
            GetComponent<AudioSource>().Play();
			_speedMod = 8f;
			yield return new WaitForSeconds(PffffftDuration);
			_speedMod = 1f;
		}

		public void StartFloat()
		{
			XScale *= 0.1f;
			YScale *= 0.1f;
			_8pos = transform.position;
			_moveIn8 = true;
		}

		IEnumerator MoveTowards(Vector3 start, Vector3 target, float duration, Quaternion rotation, float delay = 0f)
		{
			if(delay > 0f) yield return new WaitForSeconds(delay);
			float t = 0f;
			var startRot = transform.rotation;
			while (t <= 1)
			{
				t += Time.deltaTime * 1 / duration;
				transform.position = Vector3.Lerp(start, target, t);
				transform.rotation = Quaternion.Lerp(startRot, rotation, t);
				yield return null;
			}
		}

		public IEnumerator Move(Vector3 target)
		{
			OnHit.RemoveAllListeners();
			yield return MoveTowards(transform.position, target, 2f,  Quaternion.identity);
		}

		private float _count;
		private Vector3 _8pos;
		
		void Update()
		{
			if (_moveIn8)
			{
				_count += Time.deltaTime * _speedMod;
				transform.position = _8pos +
				                     (Vector3.right * Mathf.Sin(_count / 2 * Speed) * XScale -
				                      Vector3.up * Mathf.Sin(_count * Speed) * YScale);
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(
					new Vector3(0, 0, 20) * Mathf.Sin(_count / 2 * Speed) * XScale -
					new Vector3(0, 0, 0) * Mathf.Sin(_count * Speed) * YScale), _count);

			}
		}

		public void Pop()
		{	
			GetComponentInChildren<BalloonUi>().gameObject.SetActive(false);
			_anim.SetTrigger("OnPop");
		}

		public void Delete()
		{
			if(Line)Destroy(Line.gameObject);
			Destroy(gameObject);
		}
	}
}
