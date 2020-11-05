using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseControl : MonoBehaviour
{
    public static bool isGamePaused;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioSource backsound;

    void displayPauseMenu()
    {
        if (isGamePaused)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
        }
    }


    public void pauseGame()
    {
        /*
         * Pause game using time scale method
         * 
         */

        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        displayPauseMenu();
    }

    public void setVolume(float _volume)
    {
        backsound.volume = _volume;
    }

    public void restartGame()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        pauseGame();
        SceneManager.LoadScene(sceneIndex);
    }

    public void gotoMainMenu()
    {
        pauseGame();
        SceneManager.LoadScene(0);
    }
}
