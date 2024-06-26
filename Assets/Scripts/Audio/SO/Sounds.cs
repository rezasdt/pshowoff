using System;
using UnityEngine;

[CreateAssetMenu]
public class Sounds : ScriptableObject
{
    public event Action<float> SoundtrackVolumeChanged = delegate { };
    public event Action<float> SoundeffectVolumeChanged = delegate { };
    public float SoundtrackVolume
    {
        get => soundtrackVolume;
        set
        {
            soundtrackVolume = value;
            SoundtrackVolumeChanged.Invoke(value);
        }
    }
    public float SoundeffectVolume
    {
        get => soundeffectVolume;
        set
        {
            soundeffectVolume = value;
            SoundeffectVolumeChanged.Invoke(value);
        }
    }
    
    [field: Header("General")]
    [field: SerializeField] public bool Muted { get; set; } = false;


    [Header("Soundtracks")]
    [Range(0f, 1f)][SerializeField] private float initialSoundtrackVolume;
    [Range(0f, 1f)][SerializeField] private float soundtrackVolume;
    [field: SerializeField] public AudioClip StartMenu { get; private set; }
    [field: SerializeField] public AudioClip Game { get; private set; }

    [Header("Sound Effects")]
    [Range(0f, 1f)][SerializeField] private float initialSounddffectVolume;
    [Range(0f, 1f)][SerializeField] private float soundeffectVolume;
    [field: SerializeField] public AudioClip Challenge { get; private set; }
    [field: SerializeField] public AudioClip ChallengeSuccess { get; private set; }
    [field: SerializeField] public AudioClip ChallengeFail { get; private set; }
    [field: SerializeField] public AudioClip BuildSuccess { get; private set; }
    [field: SerializeField] public AudioClip BuildFail { get; private set; }
    
    private void OnEnable()
    {
        SoundtrackVolume = initialSoundtrackVolume;
        SoundeffectVolume = initialSounddffectVolume;
    }

    private void OnValidate()
    {
        SoundtrackVolumeChanged.Invoke(SoundtrackVolume);
        SoundeffectVolumeChanged.Invoke(SoundeffectVolume);
    }
}
