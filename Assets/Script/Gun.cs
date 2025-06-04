using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 20f;

    // Elimina fireAction si no lo usas para otra cosa

    public void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
            }
            else
            {
                Debug.LogWarning("El prefab de la bala no tiene Rigidbody.");
            }
        }
        else
        {
            Debug.LogWarning("BulletPrefab o FirePoint no están asignados en el script Gun.");
        }
    }
}