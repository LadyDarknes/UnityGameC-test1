using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public bool isDead => health <= 0;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (isDead)
        {
            Die();
        }
    }

    void Die()
    {
        // Ölüm animasyonu veya efektlerini ekleyin fewzi ve kerem
        Destroy(gameObject); 
    }
}
