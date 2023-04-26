using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownSlashEffect : MonoBehaviour
{
    public void Show() => gameObject.SetActive(true);
    private void HideDownSlashEffect() => gameObject.SetActive(false);
}
