using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject textObj;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Vector3 posInitial;
    [SerializeField] Vector3 posFinal;

    public int Level = 1;
    public float speed = 500f;

    RectTransform textRect;
    TextMeshProUGUI tmpText;
    Coroutine moveRoutine;

    void Awake()
    {
        textRect = textObj.GetComponent<RectTransform>();
        tmpText = textObj.GetComponent<TextMeshProUGUI>();
    }

    public void PressPause(bool PauseMenu)
    {
        pauseMenu.SetActive(PauseMenu);
        Time.timeScale = PauseMenu ? 0 : 1;
    }

    public void ActivateWinAndMoveToNext()
    {
        textObj.SetActive(true);

        tmpText.text = "Level " + Level + " Finished";

        // Start from initial position only once
        textRect.anchoredPosition = posInitial;

        MoveTo(posFinal);

        Level++;
        Invoke(nameof(LevelObjFalse), 2f);
    }

    void LevelObjFalse()
    {
        MoveTo(posInitial);
        MoveToNextLevel();
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void MoveToNextLevel()
    {

        if (Level > 4)
        {
            textObj.SetActive(true);
            tmpText.text = "Game Completed";
        }
        else
            levelText.text = "Level " + Level;
    }

    public void MoveTo(Vector3 targetPos)
    {
        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        moveRoutine = StartCoroutine(MoveRoutine(targetPos));
    }

    IEnumerator MoveRoutine(Vector3 targetPos)
    {
        while (Vector3.Distance(textRect.anchoredPosition, targetPos) > 0.01f)
        {
            textRect.anchoredPosition = Vector3.MoveTowards(
                textRect.anchoredPosition,
                targetPos,
                speed * Time.unscaledDeltaTime
            );

            yield return null;
        }

        textRect.anchoredPosition = targetPos;
    }
    public void MoveCamera(Vector3 targetPosition, float duration)
    {
        StartCoroutine(MoveCameraSmoothly(targetPosition, 2f));
    }
    public IEnumerator MoveCameraSmoothly(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        transform.position = targetPosition;
    }
}