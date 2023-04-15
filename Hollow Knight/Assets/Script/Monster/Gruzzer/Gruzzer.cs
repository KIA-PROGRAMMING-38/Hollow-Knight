using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gruzzer : MonsterController
{
    private float temporaryGravity = 2f;
    private void Awake()
    {
        base.Awake();
        moveSpeed = 6f;
        attackSpeed = 8f;
        monsterHealth = 60;
        hitDamage = 20;
        attackRange = 8f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            // �������� �Ծ��� ��
            Damage(hitDamage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            // óġ�Ǿ��� ��
            if (monsterHealth == 0)
            {
                anim.SetTrigger("Die");
                col.enabled = false;
                rigid.simulated = false;
            }

        }
    }

}
