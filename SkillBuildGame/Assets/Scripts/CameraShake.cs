using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	void Start()
	{
		Debug.Log("CameraShake script is running!");  // Check if script is running
		StartCoroutine(Shake(10f, 22f));
	}

	public IEnumerator Shake(float duration, float magnitude)
	{
		Vector3 originalPos = transform.localPosition;
		float elapsed = 0.0f;

		while (elapsed < duration)
		{
			float x = Random.Range(-1f, 1f) * magnitude;
			float y = Random.Range(-1f, 1f) * magnitude;

			transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
			elapsed += Time.deltaTime;

			yield return null; // Wait until next frame
		}

		transform.localPosition = originalPos; // Reset position
	}
}
