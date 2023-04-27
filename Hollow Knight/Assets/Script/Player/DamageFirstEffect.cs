using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFirstEffect : MonoBehaviour
{
    private DamageEffect damageEffect;
    void Start()
    {
        damageEffect = GetComponentInChildren<DamageEffect>();
    }

    public void Show() => gameObject.SetActive(true);
    private void HideDamageEffect() => gameObject.SetActive(false);
    private void ShowDamageEffect() => damageEffect.Show();
}
