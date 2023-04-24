using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] lifeImage;
    public Image mpImage;
    private int maxMp = 100;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    
    public void UpdateLifeIcon(int life)
    {
        for(int index = 0; index < 5; ++index)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }
        for(int index = 0; index < life; ++index)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1);
        }
    }
    public void ConsumptionMpIcon(float mp)
    {
        mpImage.fillAmount -= mp;
        if(mpImage.fillAmount < 0.1f)
        {
           // 마나가 떨어지면 스킬 사용 불가능 
        }
    }
    public void AcquisitionMpIcon(float mp)
    {
        mpImage.fillAmount += mp;
    }

}
