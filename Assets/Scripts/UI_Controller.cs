using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    public GameObject primaryIcon;
    public GameObject secondaryIcon;
    public GameObject switchPrompt;
    public GameObject finalTimerText;
    public Text pageText;

    public static float timeLeft;
    public static int pagesCollected;
    public static GameObject pausePanel;
    public static GameObject gameOverPanel;

    void Start()
    {
        pagesCollected = 0;
        timeLeft = 150;

        pausePanel = GameObject.Find("PausePanel");
        gameOverPanel = GameObject.Find("GameOverPanel");

        primaryIcon.SetActive(true);
        secondaryIcon.SetActive(false);
        switchPrompt.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        finalTimerText.SetActive(false);

        pageText.text = $"{pagesCollected}/6";
    }

    
    void Update()
    {
        pageText.text = $"{pagesCollected}/6";

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            AudioListener.pause = true;
        }

        if (HandGunScript.hasSecondary)
        {
            switchPrompt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Y))
            {
                primaryIcon.SetActive(false);
                secondaryIcon.SetActive(true);

                GameObject tempIcon = primaryIcon;
                primaryIcon = secondaryIcon;
                secondaryIcon = tempIcon;
            }
        }

        if(pagesCollected == 6)
        {
            timeLeft -= Time.deltaTime;
            finalTimerText.SetActive(true);
            finalTimerText.GetComponent<Text>().text = $"{(int)timeLeft}";

            if(timeLeft <= 0)
            {
                GameOver();
            }
        }
    }

    public static void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }
}
