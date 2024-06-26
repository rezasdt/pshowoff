using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; } 
    [SerializeField] private AudioSource soundtrackSource;
    [SerializeField] private AudioSource soundeffectSource;
    [field: SerializeField] public Sounds Sounds { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        soundtrackSource.volume = Sounds.SoundtrackVolume;
        soundeffectSource.volume = Sounds.SoundeffectVolume;
    }

    private void OnEnable()
    {
        Sounds.SoundtrackVolumeChanged += OnSoundtrackVolumeChanged;
        Sounds.SoundeffectVolumeChanged += OnSoundeffectVolumeChanged;
    }

    private void OnDisable()
    {
        Sounds.SoundtrackVolumeChanged -= OnSoundtrackVolumeChanged;
        Sounds.SoundeffectVolumeChanged -= OnSoundeffectVolumeChanged;
    }

    private void OnSoundtrackVolumeChanged(float pVolume)
    {
        soundtrackSource.volume = pVolume;
    }
    private void OnSoundeffectVolumeChanged(float pVolume)
    {
        soundeffectSource.volume = pVolume;
    }

    public void PlaySoundtrack(AudioClip pClip)
    {
        soundtrackSource.clip = pClip;
        soundtrackSource.Play();
    }

    public void PlaySoundeffect(AudioClip pClip)
    {
        soundeffectSource.clip = pClip;
        soundeffectSource.Play();
    }
}
