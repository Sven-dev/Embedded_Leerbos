using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorMove : MonoBehaviour {

    // Use this for initialization
    void OnAnimatorMove()
    {
        Animator anim = GetComponentInParent<Animator>();
        if (anim)
        {
            transform.parent.position = anim.rootPosition;
            transform.parent.rotation = anim.rootRotation;
        }
    }
}
