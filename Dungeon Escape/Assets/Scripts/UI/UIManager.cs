using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance != null)
            {

            }
            return _instance;
        }
    }
    public Text playerGemCount;
    public Image selectiojnImg;
    public Text gemCountText;
    public Image[] healthBars;
    public void OpenShop(int gemCount)
    {
        playerGemCount.text = "" + gemCount + "0";
    }
    public void UpdateShopSelection(int yPos)
    {
        selectiojnImg.rectTransform.anchoredPosition = new Vector2(selectiojnImg.rectTransform.anchoredPosition.x, yPos);
    }
    public void Awake()
    {
        _instance = this;
    }
    public void UpdateGemCount(int count)
    {
        gemCountText.text = "" + count;
    }
    public void UpdateLive (int livesRemaning)
    {
        for (int i = 0; i <= livesRemaning; i++)
        {
            if(i == livesRemaning)
            {
                healthBars[i].enabled = false;
            }
        }
    }
}
