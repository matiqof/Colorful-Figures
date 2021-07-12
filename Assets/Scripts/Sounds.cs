using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] private List<AudioClip> sounds;
    
    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(int sound)
    {
        _source.clip = sounds[sound];
        
        _source.Play();
    }
}
