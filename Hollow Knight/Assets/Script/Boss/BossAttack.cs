using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    void start()
    {
        HideAttackEffect();
    }

    public void Show() => gameObject.SetActive(true);
    private void HideAttackEffect() => gameObject.SetActive(false);
}
