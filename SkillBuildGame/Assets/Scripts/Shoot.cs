using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
	public GameObject bulletPrefab;
	public Transform bulletSpawnPoint;
	public float bulletSpeed;

	public void ShootBullet()
	{
		// Instantiate the bullet from the UI canvas
		GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity, bulletSpawnPoint.parent);
		//bullet.GetComponent<RectTransform>().anchoredPosition = bulletSpawnPoint.GetComponent<RectTransform>().anchoredPosition;

		// Add force to move the bullet (UI-based movement)
		bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bulletSpeed);

		// Destroy the bullet after 2 seconds to avoid clutter
		Destroy(bullet, 1f);
	}
}
