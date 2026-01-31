using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn")]
    public Transform player;
    public float spawnMinRadius = 5f;
    public float spawnMaxRadius = 20f;

    [Header("Limit")]
    public int maxEnemyCount = 5;

    [Header("Prefabs")]
    public EnemyMelee meleePrefab;
    public EnemyRanged rangedPrefab;
    public EnemySpike spikePrefab; 

    private List<Enemy> enemyPool = new();

    private void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public void SpawnEnemy()
    {
        if (enemyPool.Count >= maxEnemyCount)
            return;

        Enemy enemy = GetEnemyFromPool();
        if (enemy == null) return;

        enemy.transform.position = GetRandomSpawnPos();
        enemy.OnSpawn();

        enemyPool.Add(enemy);
    }

    private Enemy GetEnemyFromPool()
    {
        // 简单随机一种敌人
        int rand = Random.Range(0, 3);

        Enemy prefab = rand switch
        {
            0 => meleePrefab,
            1 => rangedPrefab,
            2 => spikePrefab
        };

        Enemy enemy = Instantiate(prefab, transform);
        enemy.gameObject.SetActive(false);
        return enemy;
    }

    private Vector2 GetRandomSpawnPos()
    {
        float radius = Random.Range(spawnMinRadius, spawnMaxRadius);
        float angle = Random.Range(0f, Mathf.PI * 2f);

        Vector2 offset = new Vector2(
            Mathf.Cos(angle),
            Mathf.Sin(angle)
        ) * radius;

        return (Vector2)player.position + offset;
    }
}

