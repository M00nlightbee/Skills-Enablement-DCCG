using UnityEngine;
using UnityEngine.UI;

public class Bounce : MonoBehaviour
{
	public float bounceSpeed = 2f;  // Speed of bounce
	public float bounceHeight = 30f; // Adjust for UI scale
	private Vector3 startPosition;

	void Start()
	{
		startPosition = transform.localPosition; // Use localPosition since it's UI
	}

	void Update()
	{
		float newY = startPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
		transform.localPosition = new Vector3(startPosition.x, newY, startPosition.z);
	}
}
