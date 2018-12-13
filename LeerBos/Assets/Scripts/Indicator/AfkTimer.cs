using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfkTimer : MonoBehaviour
{
    public float IdleTime;
    [Tooltip("The amount of times one of the interactables need to be hit until the players understand the controls")]
    public int DisableIn;

    private bool _active = false;
    public bool Active
    {
        get
        {
            return _active;
        }
        set
        {
            bool old = _active;
            _active = value;
            if (old == false && _active == true)
            {
                StartCoroutine(_IdleTimer());
            }
        }
    }

    public List<Interactable> Interactables;
    [Space]
    public List<TutorialIndicator> Indicators;

	// Use this for initialization
	void Awake()
    {
        VictoryScript.OnVictory += Stop;
        Active = true;
	}

    protected IEnumerator _IdleTimer()
    {
        while (Active)
        {
            float CurrentTime = 0;
            while (!Clicked() && Active)
            {
                CurrentTime += Time.deltaTime;
                if (CurrentTime > IdleTime)
                {
                    foreach (TutorialIndicator i in Indicators)
                    {
                        if (i != null)
                        {
                            i.Show();
                        }
                    }

                    while(!Clicked() && Active)
                    {
                        yield return null;
                    }

                    foreach (TutorialIndicator i in Indicators)
                    {
                        if (i != null)
                        {
                            i.Hide();
                        }
                    }

                    break;
                }

                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    //Checks if any of the interactables has been clicked
    private bool Clicked()
    {
        foreach (Interactable i in Interactables)
        {
            if (i.Clicked)
            {
                DisableIn--;
                if (DisableIn == 0)
                {
                    Stop();
                }

                return true;
            }
        }

        return false;
    }

    private void Stop()
    {
        Active = false;
    }

    private void OnDestroy()
    {
        VictoryScript.OnVictory -= Stop;
    }
}