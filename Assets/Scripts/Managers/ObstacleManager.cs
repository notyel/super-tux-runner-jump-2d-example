using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager
{
    private GameObject rock;
    private GameObject rockSmall;
    private float speed;
    private float obstacleSpawnTimer = 0f;
    private float spawnInterval = 2.5f;
    private float lastObstacleX = 10f;

    private List<GameObject> rockList = new();

    private readonly float minDistance = 5f;
    private readonly float maxDistance = 7f;
    private readonly float sequenceChance = 0.6f; // 30% de probabilidad de doble obstáculo

    public ObstacleManager(GameObject bigRock, GameObject smallRock, float moveSpeed)
    {
        rock = bigRock;
        rockSmall = smallRock;
        speed = moveSpeed;

        rockList.Add(Object.Instantiate(rock, new Vector2(8, -2), Quaternion.identity));
        rockList.Add(Object.Instantiate(rockSmall, new Vector2(12, -2), Quaternion.identity));
    }

    public void Update()
    {
        for (int i = 0; i < rockList.Count; i++)
        {
            if (rockList[i].transform.position.x < -10)
            {
                Object.Destroy(rockList[i]);
                rockList.RemoveAt(i);
                i--;
                continue;
            }

            rockList[i].transform.position += Vector3.left * speed * Time.deltaTime;
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

        // Genera obstáculo principal
        GameObject prefab1 = Random.value < 0.5f ? rock : rockSmall;
        GameObject obstacle1 = Object.Instantiate(prefab1, new Vector3(spawnX, y, 0), Quaternion.identity);
        rockList.Add(obstacle1);
        lastObstacleX = spawnX;

        // Con probabilidad, genera un segundo obstáculo con espacio suficiente para caer y volver a saltar
        if (Random.value < sequenceChance)
        {
            float sequenceSpacing = Random.Range(3.0f, 4.0f); // Distancia mínima de 3 unidades
            GameObject prefab2 = Random.value < 0.5f ? rock : rockSmall;
            GameObject obstacle2 = Object.Instantiate(prefab2, new Vector3(spawnX + sequenceSpacing, y, 0), Quaternion.identity);
            rockList.Add(obstacle2);
            lastObstacleX = spawnX + sequenceSpacing;
        }
    }
}
