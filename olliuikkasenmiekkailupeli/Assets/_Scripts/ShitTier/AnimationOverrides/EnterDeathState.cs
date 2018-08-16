using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDeathState : StateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Dead", true);
        GameHandler.instance.BattleEnded = true;
        Debug.Log("Battle ended");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 1f));
    }
}
