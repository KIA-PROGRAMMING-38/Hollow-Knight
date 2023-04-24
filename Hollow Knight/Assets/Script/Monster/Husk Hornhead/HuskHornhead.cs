using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuskHornhead : MonsterController
{
    [SerializeField] private UIManager uiManager;
    private void Awake()
    {
        base.Awake();
        moveSpeed = 3f;
        attackSpeed = 10f;
        monsterHealth = 60;
        hitDamage = 20;
        attackRange = 7f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            // 데미지를 입었을 때
            Damage(hitDamage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            // 처치되었을 때
            if (monsterHealth == 0)
            {
                anim.SetTrigger("Die");
                col.enabled = false;
                rigid.simulated = false;
                spriteRenderer.sortingOrder = -1;
                uiManager.AcquisitionMpIcon(0.3f);
            }

        }
    }

}
