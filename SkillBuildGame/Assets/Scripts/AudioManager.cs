using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioClip collisionSound;
	private AudioSource audioSource;
	private CameraShake cameraShake;

	void Update()
	{
		audioSource = GetComponent<AudioSource>();
		if (audioSource == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
		}
		audioSource.playOnAwake = false;

		// Find the CameraShake script in the scene
		cameraShake = Camera.main.GetComponent<CameraShake>();
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collisionSound != null)
		{
			audioSource.PlayOneShot(collisionSound);
		}

		// Trigger screen shake
		if (cameraShake != null)
		{
			StartCoroutine(cameraShake.Shake(0.2f, 0.1f));
		}
	}

	void OnTriggerEnter2D(Collider2D other)
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
}
