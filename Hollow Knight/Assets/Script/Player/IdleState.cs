using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : StateMachineBehaviour
{
    PlayerController player;
    private bool isHeal = false;
    private float keyDownTime = 0f;
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

        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("isDash", true);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            keyDownTime = Time.time;
            isHeal = false;
        }

        if(Input.GetKey(KeyCode.A))
        {
            if(Time.time - keyDownTime >= 0.5f)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("isHeal", true);
                isHeal = true;
            }
        }

        if(Input.GetKeyUp(KeyCode.A) && !isHeal)
        {
            animator.SetTrigger("isFireBall");
        }

            
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    
}
