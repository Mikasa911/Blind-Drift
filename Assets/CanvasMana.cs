using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasMana : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;
    public void LoadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    private const string TimeKey = "BestTime";

    void Start()
    {
        LoadHighScore();
    }

    void LoadHighScore()
    {
        if (PlayerPrefs.HasKey(TimeKey))
        {
            float savedTime = PlayerPrefs.GetFloat(TimeKey);

            int minutes = Mathf.FloorToInt(savedTime / 60);
            int seconds = Mathf.FloorToInt(savedTime % 60);

            highScoreText.text = $"Best Time    "+": {minutes}:{seconds:00}";
        }
        else
        {
            highScoreText.text = "Best Time    "+" : None";
        }
    }
}
