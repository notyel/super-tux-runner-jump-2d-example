using System.Collections.Generic;
using UnityEngine;

public class GroundManager
{
    private GameObject groundPrefab;
    private float speed;
    private float blockWidth = 1.0f;
    private List<GameObject> groundList = new List<GameObject>();

    public GroundManager(GameObject prefab, float moveSpeed)
    {
        groundPrefab = prefab;
        speed = moveSpeed;

        for (int i = 0; i < 21; i++)
        {
            groundList.Add(Object.Instantiate(groundPrefab, new Vector2(-10 + i * blockWidth, -3), Quaternion.identity));
        }
    }

    public void Update()
    {
        for (int i = 0; i < groundList.Count; i++)
        {
            var ground = groundList[i];

            if (ground.transform.position.x < -10 - blockWidth)
            {
                // Encuentra el ground más a la derecha
                float maxX = float.MinValue;
                foreach (var g in groundList)
                {
                    if (g.transform.position.x > maxX)
                        maxX = g.transform.position.x;
                }

                ground.transform.position = new Vector2(maxX + blockWidth, -3);
            }

            ground.transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
}
