using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IpointerEnter : MonoBehaviour, IPointerEnterHandler
{
    public Image leftImage;
    public Image rightImage;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        leftImage.gameObject.SetActive(true);
        rightImage.gameObject.SetActive(true);
    }
}
