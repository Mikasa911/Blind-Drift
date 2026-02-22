using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    public float elapsedTime = 0f;
    private bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text = "Time " + "    "+": "+minutes.ToString() + ":" + seconds.ToString("00");
    }
    void Awake()
    {
        timerText=GetComponent<TextMeshProUGUI>();
    }
    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
    }
}