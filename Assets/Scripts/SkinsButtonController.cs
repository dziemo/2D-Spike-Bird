using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinsButtonController : MonoBehaviour
{
    public int cost = 9999;
    public Color purchasedColor;
    public Sprite skinSprite;
    public Image skinImage, lockIcon, coinIcon;
    public TextMeshProUGUI costText;
    public Sprite unlockedIcon, purchasedIcon;
    bool isPurchased;

    private void Start()
    {
        costText.text = cost.ToString();
        skinImage.sprite = skinSprite;   
    }

    public void SetPurchased ()
    {
        isPurchased = true;
        lockIcon.sprite = unlockedIcon;
        lockIcon.color = purchasedColor;
        coinIcon.sprite = purchasedIcon;
        costText.color = purchasedColor;
    }

    public void ButtonAction ()
    {
        if (isPurchased)
        {
            PlayerController.instance.ChangeSkin(skinSprite);
            MenuController.instance.OpenMenu(MenuType.Main);
        } else
        {
            int playerCoins = PlayerPrefs.GetInt("Coins", 0);
            if (playerCoins >= cost)
            {
                SetPurchased();
                PlayerPrefs.SetInt("Coins", playerCoins - cost);
                GameController.instance.playerCoinsTotalText.text = PlayerPrefs.GetInt("Coins").ToString();
                SkinsController.instance.UnlockSkin(gameObject);
            } else
            {
                //NOT ENOUGH MONEY
            }
        }
    }
}
