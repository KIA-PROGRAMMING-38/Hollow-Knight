using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpSlashEffect : MonoBehaviour
{
    private HitEffect hitEffect;
    private void Start()
    {
        hitEffect = GetComponentInChildren<HitEffect>();
    }
    // ���� ���� ����Ʈ ����
    public void Show() => gameObject.SetActive(true);
    // ���� ���� ����Ʈ ����
    private void HideUpSlashEffect() => gameObject.SetActive(false);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.CompareTag("Monster"))
        {
            hitEffect.Show();
        }
    }
}
