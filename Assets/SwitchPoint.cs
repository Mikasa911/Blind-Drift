using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SwitchPoint : MonoBehaviour
{
    [SerializeField] Sprite onSprite;
    [SerializeField] Sprite offSprite;
    [SerializeField] GameObject[] inVisibleGameObjectsGroup;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ActivateSwitch(true);
            ChangeSprite(true);

        }
    }
    void ChangeSprite(bool IsActive)
    {
         PlayerMove playermove=FindAnyObjectByType<PlayerMove>();
    FindAnyObjectByType<CameraAudioClipPlayer>().PlayClip(playermove.switchclip);
        if (IsActive)
        {
            GetComponent<SpriteRenderer>().sprite = onSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = offSprite;
        }
    }
    void Start()
    {
        ActivateSwitch(false);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            ChangeSprite(false);
            ActivateSwitch(false);
        }
    }
    void ActivateSwitch(bool isVisible)
    {
        foreach (GameObject g in inVisibleGameObjectsGroup)
        {
            if (g.CompareTag("Spikes"))
            {
                DisableSpikes(g, isVisible);
            }
            else
            {
                SpriteRenderer sr = g.GetComponent<SpriteRenderer>();
                sr.enabled = isVisible;
            }
        }
    }
    void DisableSpikes(GameObject Spikes, bool isVisible)
    {
        SpriteRenderer[] srs = Spikes.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in srs)
        {     
            sr.enabled = isVisible;
        }
    }
}
