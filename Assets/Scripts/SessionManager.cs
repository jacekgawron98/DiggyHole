using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour
{
    public static SessionManager instance;
    public static bool isBeginning = true;
    public GameObject shopCanvas;
    public GameObject tutorialCanvas;

    public TMPro.TextMeshProUGUI shopText;

    void Start()
    {
        instance = this;
    }
    
    public void Replay()
    {
        if (isBeginning)
        {
            PlayerData.maxTime = 10;
        }
        isBeginning = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
