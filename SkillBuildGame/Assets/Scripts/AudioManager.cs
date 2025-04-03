using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioClip backgroundMusic;
	public AudioClip collisionSound;
	public AudioClip damageSoundEffect;
	public AudioClip healSoundEffect;

	private AudioSource musicSource;
	private AudioSource sfxSource;

	private void Awake()
	{
		// Ensure we have AudioSource components
		musicSource = gameObject.AddComponent<AudioSource>();
		sfxSource = gameObject.AddComponent<AudioSource>();

		// Setup AudioSource properties
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "DefeatScene" || UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "VictoryScene")
		{
			musicSource.loop = false;
		}
		else
		{
			musicSource.loop = true;
		}
		musicSource.playOnAwake = false;
		musicSource.volume = 0.5f;

		sfxSource.playOnAwake = false;
		sfxSource.volume = 0.5f;
	}

	private void Start()
	{
		// Play background music if assigned
		if (backgroundMusic != null)
		{
			musicSource.clip = backgroundMusic;
			musicSource.Play();
		}
	}

	public void PlayClip(AudioClip clip)
	{
		if (clip != null)
		{
			sfxSource.PlayOneShot(clip);
		}
	}

	public void SetMusicVolume(float volume)
	{
		musicSource.volume = Mathf.Clamp(volume, 0f, 1f);
	}

	public void SetSFXVolume(float volume)
	{
		sfxSource.volume = Mathf.Clamp(volume, 0f, 1f);
	}

	public float GetMusicVolume()
	{
		return musicSource.volume;
	}

	public float GetSFXVolume()
	{
		return sfxSource.volume;
	}
}
