using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpSlashEffect : MonoBehaviour
{
    // ���� ���� ����Ʈ ����
    public void Show() => gameObject.SetActive(true);
    // ���� ���� ����Ʈ ����
    private void HideUpSlashEffect() => gameObject.SetActive(false);
}
