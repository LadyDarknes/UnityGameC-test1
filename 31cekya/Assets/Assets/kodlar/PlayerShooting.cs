using UnityEngine;
using System.Collections; // IEnumerator için gerekli

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Mermi prefab'ı
    public Transform firePoint; // Ateş etme noktası
    public float bulletSpeed = 20f; // Mermi hızı
    public int maxAmmo = 10; // Maksimum mermi sayısı
    public float reloadTime = 2f; // Yeniden yükleme süresi

    private Animator animator;
    private int currentAmmo;
    private bool isReloading = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentAmmo = maxAmmo; // Başlangıçta tam dolu şarjör
        Cursor.lockState = CursorLockMode.Locked; // Fare imlecini kilitle
        Cursor.visible = false; // Fare imlecini gizle
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

        if (Input.GetButtonDown("Fire1")) // Fire1 varsayılan olarak sol fare tuşudur
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None; // Fare imlecini serbest bırak
            Cursor.visible = true; // Fare imlecini göster
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked; // Fare imlecini tekrar kilitle
            Cursor.visible = false; // Fare imlecini tekrar gizle
        }
    }

    void Shoot()
    {
        animator.SetTrigger("isShooting"); // Ateş etme animasyonunu tetikle

        currentAmmo--; // Mermi sayısını azalt

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed; // Mermiye hız ver

        // Mermiyi belirli bir süre sonra yok et (isteğe bağlı)
        Destroy(bullet, 2.0f);
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("isReloading", true); // Yeniden yükleme animasyonunu tetikle

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