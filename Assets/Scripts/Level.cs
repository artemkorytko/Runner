using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject finishPrefab;
    [SerializeField] private GameObject wallPrefab;

    [Header("Road settings")]
    [SerializeField] private int floorCount = 10;
    [SerializeField] private float floorLength = 5f;
    [SerializeField] private float floorWidth = 6f;

    [Header("Wall settings")]
    [SerializeField] private float wallMinOffset = 3f;
    [SerializeField] private float wallMaxOffset = 5f;

    private PlayerController player;
    public PlayerController Player => player;

    private Vector3 playerLocalPosition = Vector3.zero;

    public void GenerateLevel()
    {
        ClearLevel();
        GenerateRoad();
        GenerateWalls();
        GeneratePlayer();
    }

    public void StartGame()
    {
        player.IsActive = true;
    }

    private void GenerateRoad()
    {
        Vector3 startPoint = Vector3.zero;
        for (int i = 0; i < floorCount; i++)
        {
            var part = Instantiate(floorPrefab, transform);
            part.transform.localPosition = startPoint;
            part.transform.localScale = new Vector3(floorWidth, part.transform.localScale.y, floorLength);

            startPoint.z += floorLength;
        }
        GenerateFinish(startPoint);
    }

    private void GeneratePlayer()
    {
        player = Instantiate(playerPrefab, transform).GetComponent<PlayerController>();
        player.SetupRoadWith(floorWidth);
    }

    private void GenerateWalls()
    {
        float fullLength = floorCount * floorLength;
        float midOfFloor = floorLength * 0.5f;
        float offsetX = floorWidth / 3f;
        float startPosZ = floorLength;
        float endPosZ = fullLength - floorLength;

        while (startPosZ < endPosZ)
        {
            var noiseZ = Random.Range(wallMinOffset, wallMaxOffset);
            var startZ = startPosZ + noiseZ;

            var randomX = Random.Range(0, 3);
            var startX = 0f;
            startX = randomX == 0? 0: randomX == 1 ? -offsetX : offsetX;

            GameObject wall = Instantiate(wallPrefab, transform);
            Vector3 localPos = Vector3.zero;
            localPos.x = startX;
            localPos.z = startZ;

            wall.transform.localPosition = localPos;
            startPosZ += floorLength;
        }
    }
    private void GenerateFinish(Vector3 startPoint)
    {
        var finish = Instantiate(finishPrefab, transform);
        finish.transform.localPosition = startPoint;
    }
    private void ClearLevel()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        player = null;
    }

}
