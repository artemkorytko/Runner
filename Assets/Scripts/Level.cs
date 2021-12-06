using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject finishPrefab;

    [Header("Shader Settings")]
    [SerializeField] private Shader shader;

    [Header("Road Settings")]
    [SerializeField] private int floorCount = 10;
    [SerializeField] private float floorLenght = 5;
    [SerializeField] private float floorWidth = 6;

    [Header("Wall Settings")]
    [SerializeField] private float wallMinOffset = 3f;
    [SerializeField] private float wallMaxOffset = 5f;

    [Header("Items Settings")]
    [SerializeField] private GameObject[] itemPrefabs;

    private List<GameObject> itemObjects = new List<GameObject>();

    private PlayerController player;

    public PlayerController Player => player;

    private Vector3 playerLocalPosition = Vector3.zero;


    private float maxRoadTurnX = 20f;
    private float maxRoadTurnY = 20f;
    private float currentRoadOffsetX = -20;
    private float currentRoadOffsetY = 0;
    private bool turnRight;

    private void Update()
    {
       
    }
    private void SetShader()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        
      /*  if (player.PlayerPosZ > 30 && player.PlayerPosZ < 60)
        {
            if (Mathf.Abs(currentRoadOffsetX) < maxRoadTurnX)
            {
                currentRoadOffsetX = Mathf.LerpAngle(currentRoadOffsetX, currentRoadOffsetX - 3, 1 * Time.deltaTime);
            }
        }
        else if(player.PlayerPosZ > 100 && player.PlayerPosZ < 150)
        {
            currentRoadOffsetX = Mathf.LerpAngle(currentRoadOffsetX, currentRoadOffsetX + 3, 1 * Time.deltaTime);
            currentRoadOffsetY = Mathf.LerpAngle(currentRoadOffsetY, currentRoadOffsetY - 3, 1 * Time.deltaTime);
        }


        Vector2 Offset = new Vector2(currentRoadOffsetX, currentRoadOffsetY);*/
        foreach (GameObject gameObject in allObjects)
        {
            if (gameObject.layer == 7)
            {
                gameObject.GetComponent<Renderer>().material.shader = shader;
               // gameObject.GetComponent<Renderer>().material.SetVector("_QOffset", Offset);
            }
        }
    }
    public void GenerateLevel()
    {
        ClearLevel();
        GenerateRoad();
        GenerateWalls();
        GenerateItems();
        GeneratePlayer();
        SetShader();
    }
    public void RestartLevel()
    {
        Destroy(player.gameObject);
        foreach (var item in itemObjects)
        {
            item.transform.GetChild(0).gameObject.SetActive(true);
        }
        player = null;
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
            part.transform.localScale = new Vector3(floorWidth, part.transform.localScale.y, floorLenght);

            startPoint.z += floorLenght;

        }
        GenerateFinish(startPoint);
    }
    private void GeneratePlayer()
    {
        player = Instantiate(playerPrefab, transform).GetComponent<PlayerController>();
        player.SetupRoadWidth(floorWidth);
    }
    private void GenerateWalls()
    {
        float fullLength = floorCount * floorLenght;
        float offsetX = floorWidth / 3f;
        float startPosZ = floorLenght;
        float endPosZ = fullLength - floorLenght;

        while (startPosZ < endPosZ)
        {
            var noiseZ = Random.Range(wallMinOffset, wallMaxOffset);
            var startZ = startPosZ + noiseZ;
            var randomX = Random.Range(0, 3);

            var startX = randomX == 0 ? 0 : randomX == 1 ? -offsetX : offsetX;

            GameObject wall = Instantiate(wallPrefab, transform);
            Vector3 localPos = Vector3.zero;
            localPos.x = startX;
            localPos.z = startZ;

            wall.transform.localPosition = localPos;
            startPosZ += floorLenght;
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
        itemObjects.Clear();
        currentRoadOffsetX = 0;
        currentRoadOffsetX = -20;
    }
    private void GenerateItems()
    {
        float noiseItem = Random.Range(5, 15);
        float fullLength = floorCount * floorLenght;
        float offsetX = floorWidth / 3f;
        float startPosZ = floorLenght + noiseItem;
        float endPosZ = fullLength - floorLenght;

        while (startPosZ < endPosZ)
        {
            var noiseZ = Random.Range(wallMinOffset, wallMaxOffset);
            var startZ = startPosZ + noiseZ;
            var randomX = Random.Range(0, 3);

            var startX = randomX == 0 ? 0 : randomX == 1 ? -offsetX : offsetX;

            int itemIndex = 0;
            if (itemPrefabs.Length > 0)
            {
                itemIndex = Random.Range(0, itemPrefabs.Length);
                GameObject item = Instantiate(itemPrefabs[itemIndex], transform);
                itemObjects.Add(item);

                Vector3 localPos = Vector3.zero;
                localPos.x = startX;
                localPos.z = startZ;

                item.transform.localPosition = localPos;
                startPosZ += floorLenght;

            }

        }

    }


}

