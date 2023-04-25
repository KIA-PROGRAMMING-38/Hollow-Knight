using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffect : MonoBehaviour
{
    public void Show() => gameObject.SetActive(true);
    private void HideDashEffect() => gameObject.SetActive(false);
}
