using System.Collections.Generic;
using UnityEngine;

public class CoinManager
{
    private GameObject coinPrefab;
    private float speed;
    private float spawnTimer = 0f;
    private float spawnInterval = 1.5f;

    private float lastCoinX = 10f;
    private List<GameObject> coinList = new();

    private readonly float minDistance = 3f;
    private readonly float maxDistance = 6f;
    private readonly float minY = -1f;
    private readonly float maxY = 0.35f;

    public CoinManager(GameObject prefab, float moveSpeed)
    {
        coinPrefab = prefab;
        speed = moveSpeed;

        // Spawn inicial
        for (int i = 0; i < 5; i++)
        {
            float spawnX = 8f + i * Random.Range(minDistance, maxDistance);
            float spawnY = Random.Range(minY, maxY);
            coinList.Add(Object.Instantiate(coinPrefab, new Vector2(spawnX, spawnY), Quaternion.identity));
        }
    }

    public void Update()
    {
        for (int i = 0; i < coinList.Count; i++)
        {
            if (coinList[i].transform.position.x < -10)
            {
                Object.Destroy(coinList[i]);
                coinList.RemoveAt(i);
                i--;
                continue;
            }

            coinList[i].transform.position += Vector3.left * speed * Time.deltaTime;
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            // 25% probabilidad de arco
            if (Random.value < 0.25f)
            {
                SpawnCoinArc();
            }
            else
            {
                SpawnCoin();
            }
            
            spawnTimer = 0f;
        }
    }

    private void SpawnCoin()
    {
        float spawnX = lastCoinX + Random.Range(minDistance, maxDistance);
        float spawnY = Random.Range(minY, maxY);

        GameObject coin = Object.Instantiate(coinPrefab, new Vector2(spawnX, spawnY), Quaternion.identity);
        coinList.Add(coin);

        lastCoinX = spawnX;
    }

    private void SpawnCoinArc()
    {
        int coinCount = Random.Range(4, 6);
        float arcWidth = 2f;
        float stepX = arcWidth / (coinCount - 1);

        float startX = lastCoinX + Random.Range(minDistance, maxDistance);

        float arcHeight = Random.Range(0.4f, 4.9f); // 0.4f; // un poco más bajo
        float peakY = maxY - Random.Range(0.5f, 1f); //1f; // el punto más alto del arco, ligeramente más bajo
        float baseY = peakY - arcHeight;

        for (int i = 0; i < coinCount; i++)
        {
            float x = startX + (i * stepX);
            float t = (float)i / (coinCount - 1);
            float y = baseY + Mathf.Sin(t * Mathf.PI) * arcHeight;

            y = Mathf.Clamp(y, minY, maxY);

            Vector2 position = new Vector2(x, y);

            bool overlaps = coinList.Exists(c => Mathf.Abs(c.transform.position.x - x) < 0.5f && Mathf.Abs(c.transform.position.y - y) < 0.5f);
            if (!overlaps)
            {
                GameObject coin = Object.Instantiate(coinPrefab, position, Quaternion.identity);
                coinList.Add(coin);
            }

            lastCoinX = x;
        }
    }
}
