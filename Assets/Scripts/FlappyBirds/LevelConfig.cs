using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "LevelConfig")]
public class LevelConfig : MonoBehaviour
{
    [SerializeField] private ObstacleController obstacleControllerPrefab;
    [SerializeField] private int maxObstacleCount = 6;
    [SerializeField] private float startX = 5f;
    [SerializeField] private float finishX = -5f;
    [SerializeField] private float offset = -2f;
    [SerializeField] private float moveSpeed = -3f;

    private List<ObstacleController> obstacleControllers = new List<ObstacleController>();
    public void CreateObstacle()
    {
        var obst = Instantiate(obstacleControllerPrefab, new Vector3(startX, 0, 0), Quaternion.identity);
        obstacleControllers.Add(obst);
        obst.Setup();
    }
    public void MoveObstacles()
    {
        for (int i = 0; i < obstacleControllers.Count; i++)
        {
            var obst = obstacleControllers[i];
            var pos = obst.transform.position;
            pos.x += moveSpeed * Time.deltaTime;

            if (pos.x < finishX)
            {
                pos.x = startX;
               
                obst.Setup();
            }
            obst.transform.position = pos;
        }

        if (obstacleControllers.Count < maxObstacleCount)
        {
            var obst = obstacleControllers[obstacleControllers.Count - 1];
            if (obst.transform.position.x < startX + offset)
            {
                CreateObstacle();
            }
        }
    }
}
