using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public GameObject roadPrefab;
    public GameObject leftBorderPrefab;
    public GameObject rightBorderPrefab;
    public GameObject grassPrefab;
    public GameObject car;
    public GameObject nitroPrefab;
    public GameObject heartPrefab;

    public int roadLength = 200;

    private float segmentLength;
    private float roadWidth;
    public int nitroCount;

    public int heartCount;

    private void Start()
    {
        GetMapSizes();
        GenerateRoad();
        GenerateCollectibles();
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
        if (nitroCount == 0) {
            nitroCount = roadLength / 30;
        }
        for (int i = 0; i < nitroCount; i++)
        {
            float yPos = Random.Range(-roadLength / 2, roadLength / 2) * segmentLength;
            float xPos = Random.Range(-roadWidth / 3, roadWidth / 3);

            Instantiate(nitroPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity, transform);
        }

        if (heartCount == 0) {
            heartCount = roadLength / 40;
        }
        for (int i = 0; i < heartCount; i++)
        {
            float yPos = Random.Range(-roadLength / 2, roadLength / 2) * segmentLength;
            float xPos = Random.Range(-roadWidth / 3, roadWidth / 3);

            Instantiate(heartPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity, transform);
        }
    }
}
