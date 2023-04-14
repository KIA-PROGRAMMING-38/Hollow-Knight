using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class MonsterWalk : StateMachineBehaviour
{
    internal float patrolMinSec = 3f;
    internal float patrolMaxSec = 5f;
    internal float patrolSec;

    internal MonsterController monster;
    internal Transform monsterTransform;

    internal Vector3 nextPoint;
    internal int currentPointIndex = 0;
    internal float elapsedTime;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monster = animator.GetComponent<MonsterController>();
        monsterTransform = animator.GetComponent<Transform>();
        Init();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float delta = Time.deltaTime;
        float step = monster.moveSpeed * delta;
        elapsedTime += delta;

        Attack(animator);

        if (elapsedTime >= patrolSec)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);
            return;
        }

        monsterTransform.position = Vector3.MoveTowards(monster.transform.position, nextPoint, step);
        if(Vector3.Distance(monster.transform.position, nextPoint) < 0.1f)
        {
            SetNextPoint();
        }

    }

    private void SetNextPoint()
    {
        nextPoint = monster.patrolPoints[currentPointIndex].position;
        currentPointIndex = (currentPointIndex + 1) % 2;
        if (currentPointIndex == 0)
            monster.spriteRenderer.flipX = true;
        else
            monster.spriteRenderer.flipX = false;
    }
    private void Init()
    {
        patrolSec = Random.Range(patrolMinSec, patrolMaxSec);
        SetNextPoint();
        elapsedTime = 0f;
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
