using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : StateMachineBehaviour
{
    PlayerController player;
    Rigidbody2D rigid;
    private bool isHeal = false;
    private float keyDownTime = 0f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<PlayerController>();
        rigid = animator.GetComponent<Rigidbody2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("isRunning", true);
        }
        else if (rigid.velocity.x == 0)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("Idle", true);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isDash", true);
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            keyDownTime = Time.time;
            isHeal = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (Time.time - keyDownTime >= 0.5f)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("isHeal", true);
                isHeal = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.A) && !isHeal)
        {
            animator.SetTrigger("isFireBall");
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
