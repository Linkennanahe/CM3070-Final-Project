using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private int score = 0;
    private int highScore = 0;
    private int coins = 0;
    private GameObject gameOverCanvas;

    // PlayerPrefs keys
    private const string HighScoreKey = "HighScore";

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(GameManager).Name;
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    void Start()
    {
        // Find the GameObject dynamically in the scene hierarchy during Start
        gameOverCanvas = GameObject.Find("GameOverCanvas");

        // Load the best score from PlayerPrefs
        LoadBestScore();

        // Assuming you want to hide the buttons at the start of the game
        HideGameOverButtons();

        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
    }

    public void GameOver()
    {
        // Display the game over UI when game over conditions are met

        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);

            // Freeze gameplay by setting time scale to 0
            Time.timeScale = 0;

            // Show game over buttons
            ShowGameOverButtons();

            // Update best score
            UpdateBestScore();
        }
    }

    public void RetryButtonClicked()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void MainMenuButtonClicked()
    {
        // Load the main menu scene
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1;
    }

    private void ShowGameOverButtons()
    {
        GameObject retryButton = GameObject.Find("RetryButton");
        GameObject mainMenuButton = GameObject.Find("MainMenuButton");

        if (retryButton != null)
        {
            retryButton.SetActive(true);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.SetActive(true);
        }
    }

    private void HideGameOverButtons()
    {
        GameObject retryButton = GameObject.Find("RetryButton");
        GameObject mainMenuButton = GameObject.Find("MainMenuButton");

        if (retryButton != null)
        {
            retryButton.SetActive(false);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.SetActive(false);
        }
    }

    public void StartGame()
    {
        // Reset the game state and hide buttons
        Time.timeScale = 1;
        score = 0;
        coins = 0;
        UpdateScoreUI();
        UpdateCoinsUI();
        HideGameOverButtons();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateHighScore();
        UpdateScoreUI();
    }

    public void AddCoins(int amount)
    {
        coins += 2*amount;
        UpdateCoinsUI();
    }

    private void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            UpdateHighScoreUI();
        }
    }

    private void UpdateScoreUI()
    {
        if (Score.instance != null)
        {
            Score.instance.UpdateCurrentScore(score);
        }
    }

    private void UpdateHighScoreUI()
    {
        if (Score.instance != null)
        {
            Score.instance.UpdateHighScore(highScore);
        }
    }

    private void UpdateCoinsUI()
    {
        if (Score.instance != null)
        {
            Score.instance.UpdateCoins(coins);
        }
    }

    private void UpdateBestScore()
    {
        if (highScore > PlayerPrefs.GetInt(HighScoreKey, 0))
        {
            PlayerPrefs.SetInt(HighScoreKey, highScore);
            PlayerPrefs.Save();
        }
    }

    private void LoadBestScore()
    {
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        UpdateHighScoreUI();
    }
}
