using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireBallEffect : MonoBehaviour
{
    public void Show() => gameObject.SetActive(true);
    private void HideEffect() => gameObject.SetActive(false);
}
