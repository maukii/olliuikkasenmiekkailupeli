using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDeathState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Dead", true);
        GameHandler.instance.BattleEnded = true;
        Debug.Log("Battle ended");
    }
}
