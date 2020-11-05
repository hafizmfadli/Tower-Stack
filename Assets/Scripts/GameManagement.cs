using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManagement : MonoBehaviour
{
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private Text scoreUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Text finalScoreUI;
    [SerializeField] private GameObject congratsUI;
    private float score;


    private void FixedUpdate()
    {
        score = cameraMovement.getHeight();
        score = Mathf.Round(score);
        scoreUI.text = score.ToString();

        //if(score > PlayerPrefs.GetFloat("Highscore", 0f))
        //{
            // saving user highscore
          //  PlayerPrefs.SetFloat("Highscore", score);
        //}
    }

    public void displayGameOverUI()
    {
        gameOverUI.SetActive(true);

        float final_height = getCurrentHeight();
        float high_score = PlayerPrefs.GetFloat("Highscore", 0f);
        if(final_height > high_score)
        {
            // update highscore
            PlayerPrefs.SetFloat("Highscore", final_height);

            // say congrats to player
            congratsUI.SetActive(true);
        }
        else
        {
            // dont say congrats to player
            congratsUI.SetActive(false);
        }
        finalScoreUI.text = "Score : " + final_height.ToString() + " m ";

        
    }

    public int getCurrentHeight()
    {
        int height = int.Parse(scoreUI.text);
        return height;
    }

    

}
