using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    private HitEffect hitEffect;
    private void Awake()
    {
        hitEffect = GetComponentInChildren<HitEffect>();
    }
    private void Start()
    {
        HideSlashEffect();
    }
    // ���� ����Ʈ ����
    public void Show() => gameObject.SetActive(true);
    // ���� ����Ʈ ����
    private void HideSlashEffect() => gameObject.SetActive(false);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision !=null && collision.CompareTag("Monster"))
        {
            hitEffect.Show();
        }
    }
}
