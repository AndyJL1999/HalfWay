using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScript : MonoBehaviour
{
    public GameObject endText;
    public GameObject farewellObject;

    void Start()
    {
        endText.SetActive(false);
        farewellObject.SetActive(false);
    }

    void Update()
    {
        if(EnemyManager.enemies.Length == 0 && EnemyManager.bosses.Length == 0 && UI_Controller.pagesCollected == 6 && EnemyManager.roundSet == true)
        {
            endText.SetActive(true);
            farewellObject.SetActive(true);
        }
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("MainMenu");
        Cursor.lockState = CursorLockMode.None;
    }
}
