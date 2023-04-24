using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    // °ø°Ý ÀÌÆåÆ® »ý¼º
    public void Show() => gameObject.SetActive(true);
    // °ø°Ý ÀÌÆåÆ® ²ô±â
    private void HideSlashEffect() => gameObject.SetActive(false);
}
