using UnityEngine;

public class DragObject : MonoBehaviour
{
	private bool isDragging;
	private new Collider2D collider2D;
	public string dropTag = "SlotArea";
	private Vector2 offset, initialPosition;

	public HealthBar healthBar;

	private void Awake()
	{
		initialPosition = transform.position;
		collider2D = GetComponent<Collider2D>();
	}

	void Update()
	{
		if (!isDragging) return;

		Vector2 mousePosition = GetMousePosition();
		transform.position = mousePosition - offset;
	}

	private void OnMouseDown()
	{
		isDragging = true;
		offset = GetMousePosition() - (Vector2)transform.position;
	}

	private void OnMouseUp()
	{
		isDragging = false;
		collider2D.enabled = false;

		RaycastHit2D hit = Physics2D.Raycast(GetMousePosition(), Vector2.zero);
		if (hit.collider != null && hit.collider.CompareTag(dropTag))
		{
			transform.position = hit.transform.position + new Vector3(0, 0, -0.1f);
			Debug.Log("Card placed in slot!");

			// Decrease health on collision with the slot area
			if (healthBar != null)
			{
				int currentHealth = (int)healthBar.slider.value;
				Debug.Log("Current Health: " + currentHealth);
				healthBar.SetHealth(currentHealth - 10);
				Debug.Log("Updated Health: " + healthBar.slider.value);
			}
		}
		else
		{
			transform.position = initialPosition;
		}

		collider2D.enabled = true;
	}

	Vector2 GetMousePosition()
	{
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
}

