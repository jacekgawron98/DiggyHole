using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI goldCount;
    public TextMeshProUGUI dynamiteCount;
    public GameObject dynamiteToggle;
    public GameObject restartButton;

    void Start()
    {
        if (SessionManager.isBeginning)
        {
            restartButton.SetActive(false);
        }
        if (SessionManager.isBeginning || PlayerData.dynamiteAmount == 0)
        {
            dynamiteToggle.SetActive(false);
        }
    }

    void Update()
    {
        goldCount.text = "Gold: " + PlayerData.gold;
        dynamiteCount.text = "x " + PlayerData.dynamiteAmount;
        if(PlayerData.dynamiteAmount > 0)
        {
            dynamiteToggle.SetActive(true);
        }
        else
        {
            dynamiteToggle.GetComponent<Toggle>().isOn = false;
            dynamiteToggle.SetActive(false);
        }
    }
}
