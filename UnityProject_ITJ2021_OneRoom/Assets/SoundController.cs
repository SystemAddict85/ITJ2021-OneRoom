using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class SoundController : MonoBehaviour
{
    public UnityEvent onPlay;
    [SerializeField] private AudioSource[] audioSources = new AudioSource[7];
    
    public enum SoundType { KeyPickup, KeyUse, BalloonPop, WrongItem, SlingshotPickup, ReadPages, Lever}

    public void PlayClipByType(int soundTypeInt)
    {
        PlaySound(soundTypeInt);
    }
    
    private void PlaySound(int typeInt)
    {
        var audioSource = audioSources[typeInt];
        
        if(audioSource.isPlaying)
            audioSource.Stop();

        audioSource.Play();
    }

    [SerializeField] private bool oneShot = false;

    private void Update()
    {
        if (oneShot)
        {
            oneShot = false;
            onPlay?.Invoke();
        }
    }
    
}
