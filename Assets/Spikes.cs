using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour
{
    [SerializeField] Transform SwitchPoint;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            FindAnyObjectByType<CameraAudioClipPlayer>().PlayClip(collision.gameObject.GetComponent<PlayerMove>().dieclip);
            TeleportToVisionSwitch(collision.gameObject);
        }
    }
    void TeleportToVisionSwitch(GameObject player)
    {
        Vector2 pos=SwitchPoint.position;
        player.transform.position=pos;
    }
}
