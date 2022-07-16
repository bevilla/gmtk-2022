using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioClip thunder;
    public AudioClip sword;

    AudioSource m_audioSource;

    void Awake()
    {
        Instance = this;
        m_audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioClip(AudioClip audioClip)
    {
        m_audioSource.PlayOneShot(audioClip);
    }
}
