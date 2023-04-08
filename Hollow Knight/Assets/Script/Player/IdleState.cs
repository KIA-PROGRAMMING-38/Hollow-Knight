using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : StateMachineBehaviour
{
    PlayerController player;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<PlayerController>();
       
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            animator.SetTrigger("isRunStart");
            animator.SetBool("Idle", false);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            player.StartCoroutine(ChangeAnimation(animator));
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("isDash", true);
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

        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("isFireBall");
        }
        


    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    private IEnumerator ChangeAnimation(Animator animator)
    {
        while (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("isJump", true);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
