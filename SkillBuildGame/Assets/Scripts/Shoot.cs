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
		GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity, bulletSpawnPoint.parent);
		bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bulletSpeed);
		Destroy(bullet, 1f);
	}
}
