using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void restartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void mainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
