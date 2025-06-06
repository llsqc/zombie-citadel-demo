using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic _instance;
    public static BKMusic Instance => _instance;

    private AudioSource _bkSource;

    private void Awake()
    {
        _instance = this;
        _bkSource = GetComponent<AudioSource>();

        MusicData musicData = GameDataMgr.Instance.musicData;
        SetIsOpen(musicData.isMusicOpen);
        SetVolume(musicData.musicValue);
    }

    public void SetIsOpen(bool isOpen)
    {
        _bkSource.mute = !isOpen;
    }

    public void SetVolume(float volume)
    {
        _bkSource.volume = volume;
    }
}