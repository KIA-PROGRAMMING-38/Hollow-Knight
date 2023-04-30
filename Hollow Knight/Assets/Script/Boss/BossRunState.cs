using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRunState : StateMachineBehaviour
{
    private BossController boss;
    private Vector2 runVelocity = default;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<BossController>();
        runVelocity.Set(boss.moveSpeed, 0f);
        if (boss.spriteRenderer.flipX == false)
            boss.rigid.velocity = Vector2.left * runVelocity;
        else
            boss.rigid.velocity = Vector2.right * runVelocity;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(boss.rigid.velocity.x == 0)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Idle", true);
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
