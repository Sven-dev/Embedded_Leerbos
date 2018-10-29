using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages direction & moves any coins landed on it
public class Belt : MonoBehaviour
{
    public DirectionEnum DirectionEnum;
    public float Speed;

    private List<Coin> Coins;
    private ConveyorConnector Connector;

    // Use this for initialization
    void Start ()
    {
        Coins = new List<Coin>();
        Connector = transform.parent.GetComponent<ConveyorConnector>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Coin c = collision.GetComponent<Coin>();
        if (c != null)
        {
            Coins.Add(c);
            StartCoroutine(_MoveBelt());
        }
    }

    IEnumerator _MoveBelt()
    {
        while (Connector.Active && Coins.Count > 0)
        {
            foreach (Coin c in Coins)
            {
                c.transform.Translate(Direction() * Speed * Time.deltaTime);
            }
            yield return null;
        }
    }

    Vector3 Direction()
    {
        switch (DirectionEnum)
        {
            case DirectionEnum.Up:
                return Vector3.up;
            case DirectionEnum.UpLeft:
                return Vector3.up + Vector3.left;
            case DirectionEnum.Left:
                return Vector3.left;
            case DirectionEnum.DownLeft:
                return Vector3.down + Vector3.left;
            case DirectionEnum.Down:
                return Vector3.down;
            case DirectionEnum.DownRight:
                return Vector3.down + Vector3.right;
            case DirectionEnum.Right:
                return Vector3.right;
            case DirectionEnum.UpRight:
                return Vector3.up + Vector3.right;
            default:
                throw new System.Exception("DirectionEnum not implemented");
        }
    }
}