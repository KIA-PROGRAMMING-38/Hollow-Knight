using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpState : StateMachineBehaviour
{
    PlayerController player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<PlayerController>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(player.rigid.velocity.y < 0)
        {
            animator.SetBool("isJump", false);
            animator.SetBool("isJumpDown", true);
        }
        
        else if (player.transform.position.y < 0)
        {
            animator.SetBool("isJumpDown", false);
            animator.SetBool("isRunning", true);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                animator.SetTrigger("isUpSlash");
                return;
            }
            animator.SetTrigger("isSlash");
        }
        
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
