using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpSlashEffect : MonoBehaviour
{
    private HitEffect hitEffect;
    private void Start()
    {
        hitEffect = GetComponentInChildren<HitEffect>();
        HideUpSlashEffect();
    }
    // 위로 공격 이펙트 생성
    public void Show() => gameObject.SetActive(true);
    // 위로 공격 이펙트 끄기
    private void HideUpSlashEffect() => gameObject.SetActive(false);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.CompareTag("Monster"))
        {
            hitEffect.Show();
        }
    }
}
