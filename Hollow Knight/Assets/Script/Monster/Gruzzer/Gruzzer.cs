using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gruzzer : MonsterController
{
    private float temporaryGravity = 2f;
    [SerializeField] private UIManager uiManager;
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
                uiManager.AcquisitionMpIcon(0.3f);
            }

        }
    }

}
