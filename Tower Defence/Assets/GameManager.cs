using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private int score = 0;
    private int highScore = 0;
    private int coins = 0;

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

    public void GameOver()
    {
        // Display the game over UI
        // (You might want to activate a Canvas with buttons, text, etc.)
        // For simplicity, let's assume you have a Canvas named "GameOverCanvas"
        GameObject gameOverCanvas = GameObject.Find("GameOverCanvas");
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
    }

    public void RetryButtonClicked()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButtonClicked()
    {
        // Load the main menu scene (adjust the scene name as needed)
        SceneManager.LoadScene("MainMenu");
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateHighScore();
        UpdateScoreUI();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
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
}
