using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int health = 3; // Düşmanın sağlığı
    private Transform player;
    private Text healthText;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        healthText = GameObject.Find("EnemyHealthText").GetComponent<Text>();
        UpdateHealthUI();
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Karaktere doğru dönme
            if (direction.x != 0)
            {
                transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealthUI(); // Sağlığı güncelle
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died"); // Debug log ekleyelim
        Destroy(gameObject);
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Enemy Health: " + health.ToString();
        }
    }
}
