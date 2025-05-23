using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject gameOverMenu;

    public Renderer background;
    public GameObject groundCollisionMap;
    public GameObject rock;
    public GameObject rockSmall;
    public GameObject coinNormal;
    private CoinManager coinManager;


    public bool isGameOver = false;
    public bool isGameStarted = false;

    private GroundManager groundManager;
    [SerializeField] private ObstacleData[] obstacleModels;
    private ObstacleManager obstacleManager;

    private float speed = 2.0f;

    void Start()
    {
        Application.targetFrameRate = 60;
        obstacleModels = new ObstacleData[]
        {
            new ObstacleData { prefab = rock, spawnProbability = 0.5f },
            new ObstacleData { prefab = rockSmall, spawnProbability = 0.5f }
        };

        groundManager = new GroundManager(groundCollisionMap, speed);
        obstacleManager = new ObstacleManager(obstacleModels, speed);
        coinManager = new CoinManager(coinNormal, speed);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isGameStarted)
        {
            isGameStarted = true;
        }

        if (isGameStarted && isGameOver)
        {
            startMenu.SetActive(false);
            gameOverMenu.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (isGameStarted && !isGameOver)
        {
            startMenu.SetActive(false);
            background.material.mainTextureOffset += new Vector2(0.01f, 0) * Time.deltaTime;
           
            groundManager.Update();
            obstacleManager.Update();
            coinManager.Update();
        }
    }
}