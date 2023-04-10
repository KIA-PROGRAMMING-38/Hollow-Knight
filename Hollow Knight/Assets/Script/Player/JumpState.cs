using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpState : StateMachineBehaviour
{
    PlayerController player;
    private int jumpCount;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<PlayerController>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RaycastHit2D rayHit = Physics2D.Raycast(player.rigid.position, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if(player.rigid.velocity.y < 0)
        {
            animator.SetBool("isJump", false);
            animator.SetBool("isJumpDown", true);
        }
        
        else if (rayHit.distance < 0.5f)
        {
            animator.SetBool("isJumpDown", false);
            animator.SetBool("isRunning", true);
        }

        Jump(animator);

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                animator.SetTrigger("isUpSlash");
                return;
            }

            else if (Input.GetKey(KeyCode.DownArrow))
            {
                animator.SetTrigger("isDownSlash");
                return;
            }
            animator.SetTrigger("isSlash");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("isFireBall");
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    void Jump(Animator animator)
    {
        if(player.transform.position.y < 0)
        {
            jumpCount = 1;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Input.GetKeyDown(KeyCode.Z) && jumpCount == 1)
            {
                animator.SetTrigger("isDoubleJump");
            }
            --jumpCount;
        }
    }
}
