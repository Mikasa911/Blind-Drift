using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] GameObject myLevel;
    [SerializeField] GameObject nextLevel;
    GameObject player;
    [SerializeField] Vector3 nextCameraPos;
    [SerializeField] Vector3 nextPlayerPos;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            GetComponent<Collider2D>().enabled = false;
            player = collision.gameObject;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<PlayerMove>().LockMovement();
            PlayerMove playermove = FindAnyObjectByType<PlayerMove>();
            FindAnyObjectByType<CameraAudioClipPlayer>().PlayClip(playermove.winclip);
            if (FindAnyObjectByType<UIManager>().Level == 4)
            {
                float currentTime = FindAnyObjectByType<GameTimer>().elapsedTime; // your timer variable
                HighScoreManager.SaveTime(currentTime);
                Invoke("LoadMenu", 5f);
            }
            else
            {
                nextLevel.SetActive(true);
                Invoke("DisableLevel", 2f);
            }

        }

    }
    void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void DisableLevel()
    {
        Camera.main.transform.position = nextCameraPos;
        //FindAnyObjectByType<UIManager>().MoveCamera(nextCameraPos, 2f);

        player.GetComponent<PlayerMove>().UnlockMovementDelayed();
        player.GetComponent<Collider2D>().enabled = true;
        Destroy(myLevel);
    }

}
