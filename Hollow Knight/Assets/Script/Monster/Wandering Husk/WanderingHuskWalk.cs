using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingHuskWalk : MonsterWalk
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        monster = animator.GetComponent<WanderingHusk>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Attack(animator);
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }
    private void Attack(Animator anim)
    {
        Vector2 direction = monster.target.position - monsterTransform.position;
        float distance = direction.magnitude;
        if (distance <= monster.attackRange)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Walk", false);
            anim.SetTrigger("Hit");
            monster.Flip(monster.target.position.x, monsterTransform.position.x);
        }
        return;
    }

}
