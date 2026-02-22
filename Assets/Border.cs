using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField] Transform switchPoint;
    GameObject player;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            {
                player=collision.gameObject;
                Invoke("Teleport",0f);
            }
        }
    }
    void Teleport()
    {
        Vector2 pos = switchPoint.position;
        player.transform.position = pos;
    }
}
