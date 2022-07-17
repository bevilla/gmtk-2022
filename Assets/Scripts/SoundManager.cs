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
    public AudioClip island1;
    public AudioClip island2;
    public AudioClip island3;
    public AudioClip island4;

    public AudioClip sea;

    AudioSource[] m_audioSource;

    void Awake()
    {
        Instance = this;
        m_audioSource = GetComponents<AudioSource>();
        m_audioSource[1].loop = true;
    }

    private void Start()
    {
        m_audioSource[1].clip = sea ;
        m_audioSource[1].Play();
        
    }

    public void PlayAudioClip(AudioClip audioClip)
    {
        Debug.Log(audioClip);
        m_audioSource[0].PlayOneShot(audioClip);
    }

}
