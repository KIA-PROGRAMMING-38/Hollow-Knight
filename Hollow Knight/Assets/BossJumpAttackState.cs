using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJumpAttackState : StateMachineBehaviour
{
    private BossController boss;
    private Vector2 jumpVelocity = default;
    private Vector2 frontJumpDirection = default;
    private float totalElaspedTime;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<BossController>();
        frontJumpDirection.Set(-0.2f, 1f);

        jumpVelocity = frontJumpDirection * boss.jumpForce;
        boss.rigid.velocity = jumpVelocity;
        totalElaspedTime = 0f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float delta = Time.deltaTime;
        totalElaspedTime += delta;
        RaycastHit2D ray = Physics2D.Raycast(boss.transform.position, Vector2.down, 7f * totalElaspedTime, LayerMask.GetMask("Ground"));

        if (ray)
        {
            boss.rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            animator.SetBool("JumpAttackReady", false);
            animator.SetTrigger("JumpAttack");
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("JumpAttack");
    }


}
