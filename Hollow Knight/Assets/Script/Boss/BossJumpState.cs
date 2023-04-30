using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossJumpState : StateMachineBehaviour
{
    private BossController boss;
    private Vector2 jumpVelocity = default;
    private Vector2 jumpDirection = default;
    private float totalElaspedTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<BossController>();

        if (boss.transform.localScale.x == 1)
            jumpDirection.Set(-0.2f, 1f);

        else
            jumpDirection.Set(0.2f, 1f);

        jumpVelocity = jumpDirection * boss.jumpForce;
        boss.rigid.velocity = jumpVelocity;
        totalElaspedTime = 0f;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float delta = Time.deltaTime;
        totalElaspedTime += delta;
        RaycastHit2D ray = Physics2D.Raycast(boss.transform.position, Vector2.down, 6f * totalElaspedTime, LayerMask.GetMask("Ground"));
        

        if (ray)
        {
            boss.rigid.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;
            animator.SetBool("JumpUp", false);
            animator.SetTrigger("JumpDown");
        }

        
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("JumpDown");
    }
    
    
}
