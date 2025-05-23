using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager
{
    private List<ObstacleData> obstaclePrefabs;
    private List<GameObject> activeObstacles = new();
    private float speed;
    private float obstacleSpawnTimer = 0f;
    private float spawnInterval = 2.5f;
    private float lastObstacleX = 10f;

    private readonly float minDistance = 5f;
    private readonly float maxDistance = 7f;
    private readonly float sequenceChance = 0.6f;

    public ObstacleManager(ObstacleData[] obstacles, float moveSpeed)
    {
        obstaclePrefabs = new List<ObstacleData>(obstacles);
        speed = moveSpeed;

        // Instanciar un par de obstáculos iniciales
        activeObstacles.Add(InstantiateRandomObstacle(new Vector2(8, -2)));
        activeObstacles.Add(InstantiateRandomObstacle(new Vector2(12, -2)));
    }

    public void Update()
    {
        for (int i = 0; i < activeObstacles.Count; i++)
        {
            if (activeObstacles[i].transform.position.x < -10)
            {
                Object.Destroy(activeObstacles[i]);
                activeObstacles.RemoveAt(i);
                i--;
                continue;
            }

            activeObstacles[i].transform.position += Vector3.left * speed * Time.deltaTime;
        }

        obstacleSpawnTimer += Time.deltaTime;
        if (obstacleSpawnTimer >= spawnInterval)
        {
            SpawnObstacle();
            obstacleSpawnTimer = 0f;
        }
    }

    private void SpawnObstacle()
    {
        float spawnX = lastObstacleX + Random.Range(minDistance, maxDistance);
        float y = -2f;

        GameObject obstacle1 = InstantiateRandomObstacle(new Vector2(spawnX, y));
        activeObstacles.Add(obstacle1);
        lastObstacleX = spawnX;

        if (Random.value < sequenceChance)
        {
            float spacing = Random.Range(3f, 4f);
            GameObject obstacle2 = InstantiateRandomObstacle(new Vector2(spawnX + spacing, y));
            activeObstacles.Add(obstacle2);
            lastObstacleX = spawnX + spacing;
        }
    }

    private GameObject InstantiateRandomObstacle(Vector2 position)
    {
        float total = 0f;
        foreach (var o in obstaclePrefabs)
            total += o.spawnProbability;

        float rand = Random.value * total;
        float cumulative = 0f;

        foreach (var o in obstaclePrefabs)
        {
            cumulative += o.spawnProbability;
            if (rand <= cumulative)
                return Object.Instantiate(o.prefab, position, Quaternion.identity);
        }

        // Fallback (debería no pasar si la configuración está bien)
        return Object.Instantiate(obstaclePrefabs[0].prefab, position, Quaternion.identity);
    }
}
