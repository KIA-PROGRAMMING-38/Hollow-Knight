using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    // ���� ����Ʈ ����
    public void Show() => gameObject.SetActive(true);
    // ���� ����Ʈ ����
    private void HideSlashEffect() => gameObject.SetActive(false);
}
