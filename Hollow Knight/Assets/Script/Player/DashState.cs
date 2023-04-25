using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : StateMachineBehaviour
{
    PlayerController player;
    private float dashSpeed = 30f;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<PlayerController>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            animator.SetBool("isDash", false);
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
