using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    private HitEffect hitEffect;
    private void Start()
    {
        hitEffect = GetComponentInChildren<HitEffect>();
    }
    // °ø°Ý ÀÌÆåÆ® »ý¼º
    public void Show() => gameObject.SetActive(true);
    // °ø°Ý ÀÌÆåÆ® ²ô±â
    private void HideSlashEffect() => gameObject.SetActive(false);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision !=null && collision.CompareTag("Monster"))
        {
            hitEffect.Show();
        }
    }
}
