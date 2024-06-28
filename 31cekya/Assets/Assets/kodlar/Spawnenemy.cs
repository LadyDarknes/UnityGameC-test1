using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; 
    public Transform spawnArea; 
    public float spawnInterval = 5f; 
    public float spawnIntervalDecrease = 0.1f; 
    public float minSpawnInterval = 1f; 

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
            spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - spawnIntervalDecrease);
        }
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyPrefab = enemyPrefabs[randomIndex];
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnArea.localScale.x / 2, spawnArea.localScale.x / 2),
            Random.Range(-spawnArea.localScale.y / 2, spawnArea.localScale.y / 2),
            0
        );

        Instantiate(enemyPrefab, spawnArea.position + randomPosition, Quaternion.identity);
    }
}
