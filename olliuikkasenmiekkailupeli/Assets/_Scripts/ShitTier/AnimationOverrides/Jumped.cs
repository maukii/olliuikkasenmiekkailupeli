using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumped : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Jumped", false);
    }
}
