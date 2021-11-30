using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject finishPrefab;
    [SerializeField] private GameObject wallPrefab;
    [Header("Road settings")]
    [SerializeField] private int floorCount = 10;
    [SerializeField] private float floorLength = 5;
    [SerializeField] private float floorWidth = 6;

    [SerializeField] private float wallMinOffset = 3;
    [SerializeField] private float wallMaxOffset = 5;

    private PlayerController _player;
    private Vector3 _playerLocalePosition = Vector3.zero;

    private void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        ClearLevel();
        GenerateRoad();
        GenerateWall();
        SpawnPlayer();
        StartGame();
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

    public void StartGame()
    {
        _player.IsActive = true;
    }

    private void GenerateFinish(Vector3 startPoint)
    {
        var finish = Instantiate(finishPrefab, transform);
        finish.transform.localPosition = new Vector3(startPoint.x, startPoint.y, startPoint.z - floorLength * .5f);
    }

    private void SpawnPlayer()
    {
        _player = Instantiate(playerPrefab, transform).GetComponent<PlayerController>();
        _player.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z + floorLength * .5f);
    }

    private void GenerateWall()
    {
        float fullLength = floorCount * floorLength;
        float offsetX = floorWidth / 3;
        float startPosition = floorLength;
        float endPosition = fullLength - floorLength - 1;

        while (startPosition <= endPosition)
        {
            var noise = Random.Range(wallMinOffset, wallMaxOffset);
            var randomX = Random.Range(0, 3);
            
            var startZ = startPosition + noise;
            var startX = randomX == 0 ? 0 : randomX == 1 ? -offsetX : offsetX;

            GameObject wall = Instantiate(wallPrefab, transform);
            wall.transform.localPosition = new Vector3(startX, wall.transform.localScale.y / 2, startZ);
            startPosition += floorLength;

        }
    }

    private void ClearLevel()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }

        _player = null;
    }

}
