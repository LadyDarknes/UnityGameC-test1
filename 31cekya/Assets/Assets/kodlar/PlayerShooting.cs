using UnityEngine;
using System.Collections;
using System.Linq; // Linq kullanarak en yakın düşmanı bulmak için

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public int maxAmmo = 10;
    public float reloadTime = 2f;
    public float fireRate = 1f;
    public float detectionRadius = 10f;

    private Animator animator;
    private int currentAmmo;
    private bool isReloading = false;
    private float nextFireTime = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentAmmo = maxAmmo;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (isReloading)
            return;

        if (Time.time >= nextFireTime)
        {
            Enemy target = GetNearestEnemy();
            if (target != null && !target.isDead)
            {
                Shoot(target);
                nextFireTime = Time.time + 1f / fireRate;
            }
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Shoot(Enemy target)
    {
        animator.SetBool("isShooting", true);
        currentAmmo--;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = (target.transform.position - firePoint.position).normalized;
        rb.velocity = direction * bulletSpeed;

        Destroy(bullet, 2.0f);

        animator.SetBool("isShooting", false);
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("isReloading", true);

        yield return new WaitForSeconds(reloadTime);

        animator.SetBool("isReloading", false);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 150, 20), "Ammo: " + currentAmmo + "/" + maxAmmo);
    }

    Enemy GetNearestEnemy()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        Enemy nearestEnemy = null;
        float minDistance = float.MaxValue;

        foreach (var hitCollider in hitColliders)
        {
            Enemy enemy = hitCollider.GetComponent<Enemy>();
            if (enemy != null && !enemy.isDead)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }

        return nearestEnemy;
    }
}
