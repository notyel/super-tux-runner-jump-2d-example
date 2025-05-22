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

    public List<GameObject> groundCollisionMapList = new List<GameObject>();
    public List<GameObject> rockList = new List<GameObject>();

    public bool isGameOver = false;
    public bool isGameStarted = false;
    private float speed = 2.0f;

    void Start()
    {
        Application.targetFrameRate = 60;

        for (int i = 0; i < 21; i++)
        {
            groundCollisionMapList.Add(Instantiate(groundCollisionMap, new Vector2(-10 + i, -3), Quaternion.identity));
        }

        rockList.Add(Instantiate(rock, new Vector2(8, -2), Quaternion.identity));
        rockList.Add(Instantiate(rockSmall, new Vector2(12, -2), Quaternion.identity));
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
            Map();
        }
    }

    private void Map()
    {
        background.material.mainTextureOffset += new Vector2(0.01f, 0) * Time.deltaTime;

        for (int i = 0; i < groundCollisionMapList.Count; i++)
        {
            if (groundCollisionMapList[i].transform.position.x < -10)
            {
                groundCollisionMapList[i].transform.position = new Vector2(10, -3);
            }

            groundCollisionMapList[i].transform.position += Vector3.left * speed * Time.deltaTime;
        }

        for (int i = 0; i < rockList.Count; i++)
        {
            if (rockList[i].transform.position.x < -10)
            {
                float randomY = Random.Range(7, 18);
                rockList[i].transform.position = new Vector3(randomY, -2, 0);
            }

            rockList[i].transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
}
