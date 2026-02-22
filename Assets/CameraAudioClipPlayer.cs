using UnityEngine;

public class CameraAudioClipPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip clip)
    {
        if (clip == null) return;

        audioSource.clip = clip;
        audioSource.Play();
    }
}