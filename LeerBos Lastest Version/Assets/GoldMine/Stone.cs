using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour, I_SmartwallInteractable {

    [SerializeField] private GameObject HoleDirection;
    [SerializeField] private GameObject Train;
    [SerializeField] private Karts TrainScript;
    [SerializeField] public Audio AudioClips;
    Vector2 Direction;

	// Use this for initialization
	void Start () {
        StartCoroutine(GiveDistance());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Hit(Vector3 hitPosition)
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;

        if (Vector2.Distance(TrainScript.Train.transform.position, TrainScript.targetMiddle.position) <= 0.01f)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1f;
            transform.SetParent(Train.transform, true);
            for (int i = 0; i < Train.transform.childCount; i++)
            {
                if (Train.transform.GetChild(i).name == "Kart(Clone)")
                {
                    transform.SetSiblingIndex(i);
                    break;
                }
            }
        }
        else
        {
            
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (GetComponent<Rigidbody2D>().gravityScale == 1f)
        {
            if (col.collider.name == "BandRight" || col.collider.name == "BandLeft")
            {
                AudioClips.SecondSource.clip = AudioClips.OnGround;
                AudioClips.SecondSource.Play();
                StartCoroutine(MovingOnBand());
            }
            else if (col.collider.name == "Gold(Clone)")
            {
                StartCoroutine(MoveASide(col.gameObject));
            }
        }
    }
    IEnumerator MovingOnBand()
    {
        Direction = HoleDirection.transform.position;
        while (Vector2.Distance(transform.position, Direction) >= 1f)
        {
            transform.position = Vector2.Lerp(transform.position, Direction, Time.deltaTime * 2f);
            // print("train: "  + Train.transform.position);
            yield return null;
        }
    }
    IEnumerator MoveASide(GameObject Box)
    {
        if (transform.position.x > Box.transform.position.x)
        {
            for (int i = 0; i < 30; i++)
            {
                Box.transform.position -= new Vector3(0.05f, 0.0f, 0);
                yield return null;
            }
        }
        else
        {
            for (int i = 0; i < 30; i++)
            {
                Box.transform.position += new Vector3(0.05f, 0.0f, 0);
                yield return null;
            }
        }
    }
    public IEnumerator GiveDistance()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

}

