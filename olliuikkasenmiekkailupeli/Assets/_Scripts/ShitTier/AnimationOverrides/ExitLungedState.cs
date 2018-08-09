﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLungedState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Lunged", false);
    }
}
