using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : StateMachineBehaviour
{
    PlayerController player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<PlayerController>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player.rigid.gravityScale != 0)
        {
            animator.SetBool("isDash", false);
            animator.SetBool("isRunning", true);
        }
        
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
