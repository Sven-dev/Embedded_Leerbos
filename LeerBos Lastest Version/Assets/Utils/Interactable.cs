using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, I_SmartwallInteractable
{
    public Vector3 _hitPosition;
    public void Hit(Vector3 hitPosition)
    {
        _hitPosition = hitPosition;
        OnClick.Invoke();
        StartCoroutine(_click());
    }
    public UnityEvent OnClick;
    public bool Clicked;

    // Use this for initialization
    void Awake()
    {
        Clicked = false;
    }


    // Update is called once per frame
    public IEnumerator _click()
    {
        Clicked = true;
        yield return new WaitForSeconds(0.3f);
        Clicked = false;
    }
}
