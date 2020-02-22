using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteButton : MonoBehaviour
{
    public Player player;

    public void StartPlanting()
    {
        if (player.isPlantingDynamite)
        {
            player.isPlantingDynamite = false;
        }
        else
        {
            player.isPlantingDynamite = true;
        }
    }
}
