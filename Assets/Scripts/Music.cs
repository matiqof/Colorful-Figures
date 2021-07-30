using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Music : MonoBehaviour
{
    [SerializeField] private List<AudioClip> music;

    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();

        SetRandomMusic();
        
        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate()
    {
        //Зацикливание музыки
        
        if (Math.Abs(_source.time - _source.clip.length) < 0.1)
        {
            SetRandomMusic();
        }
    }

    private void SetRandomMusic()
    {
        _source.clip = music[Random.Range(0, music.Count)];
        
        _source.Play();
    }
}
