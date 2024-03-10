using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score instance;

    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private TextMeshProUGUI _coinText;

    private void Awake()
    {
        instance = this;
    }
    // Update the Current Score
    public void UpdateCurrentScore(int score)
    {
        _currentScoreText.text = score.ToString();
    }
    // Update the Best Score
    public void UpdateHighScore(int highScore)
    {
        _highScoreText.text = highScore.ToString();
    }
    // Update the Coins
    public void UpdateCoins(int coins)
    {
        _coinText.text = coins.ToString();
    }

}
