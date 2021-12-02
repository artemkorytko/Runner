using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject finishPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject coinPrefab;

    [Header("Road settings")] 
    [SerializeField] private int coinsOnLevel = 3;
    [SerializeField] private int floorCount = 10;
    [SerializeField] private float floorLength = 5;
    [SerializeField] private float floorWidth = 6;

    [SerializeField] private float wallMinOffset = 3;
    [SerializeField] private float wallMaxOffset = 5;

    private int _coins = 0;
    private List<GameObject> _spawnedWalls = new List<GameObject>();

    
    public PlayerController player;
    public int GetCoins => _coins;

    public void GenerateLevel()
    {
        ClearLevel();
        GenerateRoad();
        GenerateWall();
        SpawnCoin();
        SpawnPlayer();
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
        player.IsActive = true;
    }

    private void GenerateFinish(Vector3 startPoint)
    {
        var finish = Instantiate(finishPrefab, transform);
        finish.transform.localPosition = new Vector3(startPoint.x, startPoint.y, startPoint.z - floorLength * .5f);
    }

    private void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, transform).GetComponent<PlayerController>();
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + floorLength * .5f);
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
            wall.transform.localPosition = new Vector3(startX, wall.transform.localScale.y * .5f, startZ);
            startPosition += floorLength;
            
            _spawnedWalls.Add(wall);
        }
    }

    private void SpawnCoin()
    {
        int maxCoins = _spawnedWalls.Count;
        float offsetX = floorWidth / 3;
        List<Vector3> coinPosition = new List<Vector3>();
        
        for (int i = 0; i < coinsOnLevel; i++)
        {
            Vector3 wallPos = _spawnedWalls[Random.Range(0, maxCoins)].transform.position;
            float posX = 0;
            int rand = Random.Range(0, 2);
            if (wallPos.x == 0)
            {
                posX = offsetX;
                if (rand == 0)
                {
                    posX = -offsetX;
                }
            } 
            else if (wallPos.x < 0)
            {
                if (rand == 0)
                {
                    posX = offsetX;
                }
            }
            else if (wallPos.x > 0)
            {
                if (rand == 0)
                {
                    posX = -offsetX;
                }
            }
            coinPosition.Add(new Vector3(posX, 1, wallPos.z));
        }

        foreach (var pos in coinPosition)
        {
            GameObject coin = Instantiate(coinPrefab, transform);
            coin.transform.position = pos;
        }
    }

    private void ClearLevel()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }
        _spawnedWalls.Clear();
        player = null;
    }

}
