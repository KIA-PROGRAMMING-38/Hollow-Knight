using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : StateMachineBehaviour
{
    PlayerController player;
    Rigidbody2D rigid;
    
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            player.StartCoroutine(ChangeAnimation(animator));
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    private IEnumerator ChangeAnimation(Animator animator)
    {
        while (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isJump", true);
            yield return new WaitForSeconds(0.2f);
        }
    }

}
