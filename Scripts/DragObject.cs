using UnityEngine;

public class DragObject : MonoBehaviour
{
	//Vector2 initialPosition = Vector2.zero;

	//private void OnMouseDown()
	//{
	//	initialPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
	//}
	//private void OnMouseDrag()
	//{
	//	transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - initialPosition;
	//}

	private bool isDragging;

	private Vector2 offset, initialPosition;

	private void Awake()
	{
		initialPosition = transform.position;
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


	void OnMouseUp()
	{
		transform.position = initialPosition;
		isDragging = false;
	}

	Vector2 GetMousePosition()
	{
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

}

