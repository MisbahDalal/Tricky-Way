using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip backgroundMusic;
    //public AudioClip death;
    public AudioClip wallTouch;
    //public AudioClip obstacle;
    public AudioClip goal;

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }
}
