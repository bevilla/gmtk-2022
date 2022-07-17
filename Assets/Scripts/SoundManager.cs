using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioClip thunder;
    public AudioClip wind;
    public AudioClip humanCry;
    public AudioClip glass;
    public AudioClip woodWreck;
    public AudioClip sword;
    public AudioClip ratStep;
    public AudioClip Doldrums;
    public AudioClip sickHuman;
    public AudioClip ofniCrash;
    public AudioClip fightPirate;
    public AudioClip woodBox;
    public AudioClip sharkDeepSounds;
    public AudioClip thunderWind;

    AudioSource m_audioSource;

    void Awake()
    {
        Instance = this;
        m_audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioClip(AudioClip audioClip)
    {
        Debug.Log(audioClip);
        m_audioSource.PlayOneShot(audioClip);
    }
}
