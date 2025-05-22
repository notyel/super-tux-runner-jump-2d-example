using System.Collections.Generic;
using UnityEngine;

public class GroundManager
{
    private GameObject groundPrefab;
    private float speed;
    private List<GameObject> groundList = new List<GameObject>();

    public GroundManager(GameObject prefab, float moveSpeed)
    {
        groundPrefab = prefab;
        speed = moveSpeed;
        for (int i = 0; i < 21; i++)
        {
            groundList.Add(Object.Instantiate(groundPrefab, new Vector2(-10 + i, -3), Quaternion.identity));
        }
    }

    public void Update()
    {
        foreach (var ground in groundList)
        {
            if (ground.transform.position.x < -10)
            {
                ground.transform.position = new Vector2(10, -3);
            }
            ground.transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
}