using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingHusk : MonsterController
{
    private void Awake()
    {
        base.Awake();
        moveSpeed = 3f;
        attackSpeed = 10f;
        attackRange = 7f;
        monsterHealth = 60;
    }

    private void Update()
    {
        if(monsterHealth > 0)
            AttackDistance();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }
}
