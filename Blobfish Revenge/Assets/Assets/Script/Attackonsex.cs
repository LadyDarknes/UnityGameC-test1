using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private Animator animator;
    private bool attack;
    private float attackTimer;
    [SerializeField]
    private float attackCooldown;
    [SerializeField]
    private Collider2D attackCollider;
    [SerializeField]
    private float attackRange;
    private Transform nearestEnemy;

    void Start()
    {
        attack = false;
        attackCollider.enabled = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        FindNearestEnemy();
        if (nearestEnemy != null && Vector2.Distance(transform.position, nearestEnemy.position) <= attackRange)
        {
            Attack();
        }
    }

    private void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        nearestEnemy = closestEnemy;
    }

    private void Attack()
    {
        if (!attack)
        {
            attack = true;
            attackCollider.enabled = true;
            attackTimer = attackCooldown;
            animator.SetTrigger("attack");
        }
        
        if (attack)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attack = false;
                attackCollider.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit!"); // Debug log ekleyelim
            EnemyController enemy = collision.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(1); // Örneğin, 1 hasar ver
                Debug.Log("Enemy took damage"); // Debug log ekleyelim
            }
            else
            {
                Debug.Log("EnemyController script not found on enemy object"); // Debug log ekleyelim
            }
        }
    }
}
