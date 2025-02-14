using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class RoadGenerator : MonoBehaviour
{
    public GameObject roadPrefab;
    public GameObject leftBorderPrefab;
    public GameObject rightBorderPrefab;
    public GameObject grassPrefab;
    public GameObject nitroPrefab;
    public GameObject heartPrefab;
    public GameObject car;
    public GameObject enemyCar;
    public Slider progressBar;


    public int roadLength = 200;

    private float segmentLength;
    private float roadWidth;
    public int nitroCount;
    public int heartCount;
    public int enemyCount = 0;
    public int maxEnemyCount;



    private void Start()
    {
        GetMapSizes();
        GenerateRoad();
        GenerateCollectibles();

        EventManager.Instance.OnPoliceCarDespawned += () => enemyCount--;
        StartCoroutine(GenerateEnemiesGradually());

    }

    private void FixedUpdate()
    {
        if (car != null)
        {
            progressBar.value = car.transform.position.y / (roadLength * segmentLength);
        }
    }

    private void GetMapSizes()
    {
        if (roadPrefab != null)
        {
            Renderer roadRenderer = roadPrefab.GetComponent<Renderer>();
            if (roadRenderer != null)
            {
                segmentLength = roadRenderer.bounds.size.y;
                roadWidth = roadRenderer.bounds.size.x;
            }
        }
    }

    private void GenerateRoad()
    {
        for (int i = 0; i < roadLength; i++)
        {
            float yPos = (i - 10) * segmentLength;

            Instantiate(roadPrefab, new Vector3(0, yPos, 0), Quaternion.identity, transform);
            Instantiate(leftBorderPrefab, new Vector3(-roadWidth / 2, yPos, 0), Quaternion.identity, transform);
            Instantiate(rightBorderPrefab, new Vector3(roadWidth / 2, yPos, 0), Quaternion.identity, transform);

            Renderer leftBorderRenderer = leftBorderPrefab.GetComponent<Renderer>();
            Renderer rightBorderRenderer = rightBorderPrefab.GetComponent<Renderer>();
            Renderer grassRenderer = grassPrefab.GetComponent<Renderer>();

            float grassWidth = grassRenderer.bounds.size.x;
            float leftGrassXPos = (-roadWidth / 2) - (leftBorderRenderer.bounds.size.x / 2) - (grassWidth / 2);
            float rightGrassXPos = (roadWidth / 2) + (rightBorderRenderer.bounds.size.x / 2) + (grassWidth / 2);

            Instantiate(grassPrefab, new Vector3(leftGrassXPos, yPos, 0), Quaternion.identity, transform);
            Instantiate(grassPrefab, new Vector3(rightGrassXPos, yPos, 0), Quaternion.identity, transform);
        }
    }

    private void GenerateCollectibles()
    {
        if (nitroCount == 0)
        {
            nitroCount = roadLength / 30;
        }
        for (int i = 0; i < nitroCount; i++)
        {
            float yPos = Random.Range(-roadLength / 2, roadLength / 2) * segmentLength;
            float xPos = Random.Range(-roadWidth / 3, roadWidth / 3);

            Instantiate(nitroPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity, transform);
        }

        if (heartCount == 0)
        {
            heartCount = roadLength / 40;
        }
        for (int i = 0; i < heartCount; i++)
        {
            float yPos = Random.Range(-roadLength / 2, roadLength / 2) * segmentLength;
            float xPos = Random.Range(-roadWidth / 3, roadWidth / 3);

            Instantiate(heartPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity, transform);
        }
    }
    private IEnumerator GenerateEnemiesGradually()
    {
        if (maxEnemyCount == 0)
        {
            maxEnemyCount = roadLength / 20;
        }

        while (enemyCount < maxEnemyCount)
        {
            int enemiesToSpawn = Random.Range(1, 4);
            for (int i = 0; i < enemiesToSpawn && enemyCount < maxEnemyCount; i++)
            {
                EventManager.Instance.PoliceCarSpawned();
                float yPos;
                if (Random.value > 0.5f)
                {
                    yPos = car.transform.position.y + Random.Range(10f, 20f) * segmentLength;
                }
                else
                {
                    yPos = car.transform.position.y - Random.Range(10f, 20f) * segmentLength;
                }
                float xPos = Random.Range(-roadWidth / 3, roadWidth / 3);

                GameObject enemy = Instantiate(enemyCar, new Vector3(xPos, yPos, 0), Quaternion.identity, transform);
                enemy.tag = "Enemy";

                enemyCount++;
            }

            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        }
    }
}
