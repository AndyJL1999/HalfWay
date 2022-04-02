using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        AudioListener.pause = false;
    }

    public void Play()
    {
        SceneManager.LoadScene("MapLevel");
        Time.timeScale = 1;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        UI_Controller.pausePanel.SetActive(false);
        AudioListener.pause = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
