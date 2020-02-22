using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public int price = 10;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI shopText;

    private void Start()
    {
        priceText.text = "Price: " + price;
        if(price == 0)
        {
            priceText.text = "Price: X";
        }
    }

    public void AddMoreTime()
    {
        if((PlayerData.gold - price) >= 0)
        {
            PlayerData.gold -= price;
            PlayerData.maxTime++;
            price += 5;
            if(price > 9999)
            {
                price = 9999;
            }
            ShopData.timePrice = price;
            priceText.text = "Price: " + price;
            shopText.text = "Now your amazing power lasts longer!";
        }
        else
        {
            shopText.text = "You don't have enough gold";
        }
    }


    public void BuyDynamite()
    {
        if (PlayerData.gold - price >= 0)
        {
            if(PlayerData.dynamiteAmount == 99)
            {
                shopText.text = "You can't buy more";
                return;
            }
            PlayerData.gold -= price;
            PlayerData.dynamiteAmount++;
            shopText.text = "You got one dynamite!";
        }
        else
        {
            shopText.text = "You don't have enough gold";
        }
    }

    public void ExtendDynamiteRange()
    {
        if (PlayerData.gold - price >= 0)
        {
            if(PlayerData.dynamiteRange == 5)
            {
                priceText.text = "Price: X";
                shopText.text = "You can't extend more!";
                return;
            }
            PlayerData.gold -= price;
            PlayerData.dynamiteRange++;
            price *= 5;
            ShopData.dynamiteExtensionPrice = price;
            if(PlayerData.dynamiteRange < 5)
            {
                priceText.text = "Price: " + price;
            }
            else
            {
                priceText.text = "Price: X";
            }
            shopText.text = "You extended the dynamite range!";
        }
        else
        {
            shopText.text = "You don't have enough gold";
        }
    }
}
