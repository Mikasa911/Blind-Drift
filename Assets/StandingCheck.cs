using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

                collision.transform.SetParent(transform);
                Debug.Log("Player Parent Set");
              
    }

    void OnTriggerExit2D(Collider2D collision)
    {

        if (!collision.gameObject.CompareTag("Player")) return;
        collision.transform.SetParent(null);
        Debug.Log("Player Parent Removed");
    }
}
