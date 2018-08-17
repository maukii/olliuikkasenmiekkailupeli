using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HobbleOverride : StateMachineBehaviour
{

    int steps;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        steps++;
        animator.SetInteger("steps", steps);
    }
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        animator.SetLayerWeight(1, 1);
    }

}
