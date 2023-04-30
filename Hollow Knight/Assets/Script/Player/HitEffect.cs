using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    
    public void Show() => gameObject.SetActive(true);

    private void HideHitEffect() => gameObject.SetActive(false);
}
