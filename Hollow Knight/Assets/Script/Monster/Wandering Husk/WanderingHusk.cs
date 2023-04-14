using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingHusk : MonsterController
{
    private void Awake()
    {
        base.Awake();
        moveSpeed = 3f;
        attackSpeed = 7f;
        monsterHealth = 60;
        hitDamage = 20;
        attackRange = 7f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            // �������� �Ծ��� ��
            anim.SetTrigger("Hit");
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
                spriteRenderer.sortingOrder = -1;
            }

        }
    }
}