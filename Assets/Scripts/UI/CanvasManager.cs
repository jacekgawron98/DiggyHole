using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject shopCanvas;
    public GameObject tutorialCanvas;
    public GameObject finishCanvas;

    public Player player;

    private void Start()
    {
        if (SessionManager.isBeginning)
        {
            tutorialCanvas.SetActive(true);
            shopCanvas.SetActive(false);
        }
    }
    void LateUpdate()
    {
        if (player.hasMoved)
        {
            shopCanvas.SetActive(false);
            tutorialCanvas.SetActive(false);
        }
        if (player.hasFinished)
        {
            finishCanvas.SetActive(true);
        }
    }
}
