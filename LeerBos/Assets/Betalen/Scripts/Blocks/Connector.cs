using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public bool Active;
    public bool PowerUsed;
    public List<Connector> Connections;

    public delegate void ConnectionsChanged();
    public event ConnectionsChanged OnConnectionsChanged;

    private void Start()
    {
        PowerUsed = false;
    }

    public void Power()
    {
        print("Powering: " + transform.parent.name);
        if (!PowerUsed)
        {
            PowerUsed = true;
            foreach (Connector c in Connections)
            {
                if (!c.Active)
                {
                    c.Active = true;
                    c.Power();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Connector c = collision.GetComponent<Connector>();
        if (c != null)
        {
            Connections.Add(c);
            if (OnConnectionsChanged != null)
            {
                OnConnectionsChanged();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Connector c = collision.GetComponent<Connector>();
        if (c != null)
        {
            if (Connections.Contains(c))
            {
                Connections.Remove(c);
            }

            if (OnConnectionsChanged != null)
            {
                OnConnectionsChanged();
            }
        }
    }
}