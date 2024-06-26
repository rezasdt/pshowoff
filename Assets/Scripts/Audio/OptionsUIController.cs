using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUIController : MonoBehaviour
{
    [SerializeField] private GameObject generalVolumeOn;
    [SerializeField] private GameObject generalVolumeOff;
    [SerializeField] private GameObject musicPanel;
    [SerializeField] private GameObject soundeffectPanel;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundeffectSlider;
    [Header("SO")]
    [SerializeField] private Sounds sounds;

    private void Awake()
    {
        musicSlider.value = sounds.SoundtrackVolume;
        soundeffectSlider.value = sounds.SoundeffectVolume;
    }

    private void OnEnable()
    {
        if (sounds.Muted)
        {
            generalVolumeOn.SetActive(false);
            generalVolumeOff.SetActive(true);
            musicPanel.SetActive(false);
            soundeffectPanel.SetActive(false);
        }
        else
        {
            generalVolumeOn.SetActive(true);
            generalVolumeOff.SetActive(false);
            musicPanel.SetActive(true);
            soundeffectPanel.SetActive(true);
        }
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        soundeffectSlider.onValueChanged.AddListener(OnSoundeffectSliderChanged);
    }

    private void OnDisable()
    {
        musicSlider.onValueChanged.RemoveListener(OnMusicSliderChanged);
        soundeffectSlider.onValueChanged.RemoveListener(OnSoundeffectSliderChanged);
    }

    public void ToggleGeneralVolume()
    {
        if (sounds.Muted) // Then turn it on
        {
            sounds.SoundtrackVolume = musicSlider.value;
            sounds.SoundeffectVolume = soundeffectSlider.value;
            generalVolumeOn.SetActive(true);
            generalVolumeOff.SetActive(false);
            musicPanel.SetActive(true);
            soundeffectPanel.SetActive(true);
            sounds.Muted = false;
        }
        else // Then turn it off
        {
            sounds.SoundtrackVolume = 0f;
            sounds.SoundeffectVolume = 0f;
            generalVolumeOn.SetActive(false);
            generalVolumeOff.SetActive(true);
            musicPanel.SetActive(false);
            soundeffectPanel.SetActive(false);
            sounds.Muted = true;
        }
    }

    private void OnMusicSliderChanged(float pValue)
    {
        sounds.SoundtrackVolume = pValue;
    }

    private void OnSoundeffectSliderChanged(float pValue)
    {
        sounds.SoundeffectVolume = pValue;
    }
}
