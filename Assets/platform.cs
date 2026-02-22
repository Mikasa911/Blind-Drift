using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    [SerializeField]bool invisible;
    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(invisible)
            {
                GetComponent<SpriteRenderer>().enabled=true;
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(invisible)
            {
                GetComponent<SpriteRenderer>().enabled=false;
            }
        }
    }
}
