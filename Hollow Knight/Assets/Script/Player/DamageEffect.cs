using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    public void Show() => gameObject.SetActive(true);
    private void HideDamageEffect() => gameObject.SetActive(false);
}
