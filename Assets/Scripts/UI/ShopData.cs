using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopData : MonoBehaviour
{
    public Player player;
    public GameObject shopCanvas;

    public Shop timeShop;
    public Shop dynamiteExtensionShop;

    public static int timePrice = 10;
    public static int dynamiteExtensionPrice = 50;

    void Awake()
    {
        timeShop.price = timePrice;
        if(PlayerData.dynamiteRange < 5)
        {
            dynamiteExtensionShop.price = dynamiteExtensionPrice;
        }
        else
        {
            dynamiteExtensionShop.price = 0;
        }
    }

}
