using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IpointerExit : MonoBehaviour, IPointerExitHandler
{
    public Image leftImage;
    public Image rightImage;
    public void OnPointerExit(PointerEventData eventData)
    {
        leftImage.gameObject.SetActive(false);
        rightImage.gameObject.SetActive(false);
    }
}
