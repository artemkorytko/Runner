using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject finishPrefab;

    [Header("Road settings")]
    [SerializeField] private int floorCount = 10;
    [SerializeField] private float floorLength = 5f;
    [SerializeField] private float floorWidth = 6f;

    [Header("Wakk settings")]
    [SerializeField] private float wallMinOffset = 3f;
    [SerializeField] private float wallMaxOffset = 5f;

    private PlayerController player;

    private Vector3 playerLocalPosition = Vector3.zero;


    private void Start()
    {
        GenerateLevel();
        StartGame();
    }

    public void GenerateLevel()
    {
        Clear();
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
    }

    private void GenerateWalls()
    {
        float fullLength = floorCount * floorLength;
        float offsetX = floorWidth / 3f;
        float startPosZ = floorLength;
        float endPositionZ = fullLength - floorLength;

        while (startPosZ < endPositionZ)
        {
            var noiceZ = Random.Range(wallMinOffset, wallMaxOffset);
            var startZ = startPosZ + noiceZ;

            var randomX = Random.Range(0, 3);
            var startX = randomX == 0? 0 : randomX == 1 ? -offsetX : offsetX;

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

    private void Clear()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        player = null;
    }

}
