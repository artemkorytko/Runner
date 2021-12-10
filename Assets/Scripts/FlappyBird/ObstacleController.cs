using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private Transform upObstacle;
    [SerializeField] private Transform downpObstacle;


    public void Setup()
    {
        upObstacle.localPosition = new Vector2(0, Random.Range(4f, 6f));
        downpObstacle.localPosition = new Vector2(0, Random.Range(-4f, -6f));
    }
}
