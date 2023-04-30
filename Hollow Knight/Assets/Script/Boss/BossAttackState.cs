using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : StateMachineBehaviour
{
    private BossController boss;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<BossController>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(boss.bossHealth <= 500)
        {
            animator.SetTrigger("AttackRe");
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("AttackRe");
    }


}
