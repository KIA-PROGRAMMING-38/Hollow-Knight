using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownSlashEffect : MonoBehaviour
{
    private HitEffect hitEffect;
    private void Start()
    {
        hitEffect = GetComponentInChildren<HitEffect>();
    }
    public void Show() => gameObject.SetActive(true);
    private void HideDownSlashEffect() => gameObject.SetActive(false);
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.CompareTag("Monster"))
        {
            hitEffect.Show();
        }
    }
}
