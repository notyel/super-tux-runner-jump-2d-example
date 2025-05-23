using UnityEngine;

[System.Serializable]
public class ObstacleData
{
    public GameObject prefab;
    [Range(0f, 1f)]
    public float spawnProbability;
}
