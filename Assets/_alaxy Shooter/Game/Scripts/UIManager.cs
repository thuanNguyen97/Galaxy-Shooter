using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImageDisplays;
    public Text scoreText;
    public GameObject titleScreen;
    public int score;    

    public void UpdateLives(int currentLives)
    {
        //assign livesImageDisplays to the array that store our lives image
        livesImageDisplays.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score; // asssign new score the the exist score (change the score)
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        scoreText.text = "Score: "; //reset score
    }

    public void ShowLiveImages()
    {
        //livesImageDisplays.SetActive(true);
    }

    public void HideLivesImages()
    {

    }

    public void ShowScoreText()
    {

    }

    public void HideScoreText()
    {

    }
}
