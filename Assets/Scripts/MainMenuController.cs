using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Text highscore;


    private void Update()
    {
        highscore.text = PlayerPrefs.GetFloat("Highscore", 0f).ToString();
    }

    public void startButton()
    {
        SceneManager.LoadScene(1);
    }

    public void objectiveButton()
    {
        SceneManager.LoadScene(2);
    }

    public void creditsButton()
    {
        SceneManager.LoadScene(3);
    }

    public void gotoMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void quitButton()
    {
        Application.Quit();
    }
}
