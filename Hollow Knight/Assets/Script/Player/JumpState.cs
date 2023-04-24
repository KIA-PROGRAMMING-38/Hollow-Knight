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
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("isFireBall");
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    
}
