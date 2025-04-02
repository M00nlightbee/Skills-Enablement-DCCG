using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioClip backgroundMusic;
	public AudioClip collisionSound;

	private AudioSource audioSource;
	private CameraShake cameraShake;

	void Start()
	{
		// Ensure we have an AudioSource component
		audioSource = GetComponent<AudioSource>();
		if (audioSource == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
		}

		// Setup AudioSource properties
		audioSource.loop = true; // Loop the background music
		audioSource.playOnAwake = false;
		audioSource.volume = 0.5f;

		// Play background music if assigned
		if (backgroundMusic != null)
		{
			audioSource.clip = backgroundMusic;
			audioSource.Play();
		}

		// Find CameraShake script
		cameraShake = Camera.main?.GetComponent<CameraShake>();
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		PlaySoundEffect();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		PlaySoundEffect();
	}

	void PlaySoundEffect()
	{
		if (collisionSound != null)
		{
			audioSource.PlayOneShot(collisionSound);
		}

		if (cameraShake != null)
		{
			StartCoroutine(cameraShake.Shake(0.2f, 0.1f));
		}
	}

	// --- Additional Background Music Controls ---
	public void PauseMusic()
	{
		if (audioSource.isPlaying)
		{
			audioSource.Pause();
		}
	}

	public void ResumeMusic()
	{
		if (!audioSource.isPlaying)
		{
			audioSource.Play();
		}
	}

	public void StopMusic()
	{
		audioSource.Stop();
	}

	public void SetMusicVolume(float volume)
	{
		audioSource.volume = Mathf.Clamp(volume, 0f, 1f);
	}
}
