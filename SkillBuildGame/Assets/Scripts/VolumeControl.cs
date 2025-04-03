using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
	public Slider musicVolumeSlider;
	public Slider sfxVolumeSlider;
	private AudioManager audioManager;

	private void Start()
	{
		audioManager = FindAnyObjectByType<AudioManager>();

		// Initialize sliders with current volume levels
		musicVolumeSlider.value = audioManager.GetMusicVolume();
		sfxVolumeSlider.value = audioManager.GetSFXVolume();

		// Add listeners to sliders
		musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
		sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
	}

	public void SetMusicVolume(float volume)
	{
		audioManager.SetMusicVolume(volume);
	}

	public void SetSFXVolume(float volume)
	{
		audioManager.SetSFXVolume(volume);
	}
}
