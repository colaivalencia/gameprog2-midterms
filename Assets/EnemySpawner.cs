using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;
    public float spawnRadius = 10f;
    private float timer = 3f;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Instantiate(enemyPrefab, (Vector2)transform.position + Random.insideUnitCircle.normalized * spawnRadius, Quaternion.identity);
            timer = spawnInterval;
        }
    }
}