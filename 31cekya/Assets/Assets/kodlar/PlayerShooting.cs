using UnityEngine;
using System.Collections; // IEnumerator için gerekli

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public int maxAmmo = 10;
    public float reloadTime = 2f;

    private Animator animator;
    private int currentAmmo;
    private bool isReloading = false;

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

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (currentAmmo <= 0)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1")) 
        {
            StartCoroutine(Shoot());
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

    IEnumerator Shoot()
    {
        animator.SetBool("isShooting", true);

        currentAmmo--; 

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;

       
        Destroy(bullet, 2.0f);

        yield return new WaitForSeconds(0.1f); 
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
}
