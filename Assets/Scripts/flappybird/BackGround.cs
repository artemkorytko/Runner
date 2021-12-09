using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FlapyBird
{
    public class BackGround : MonoBehaviour
    {
        [Header("Gizmos settings")]
        [SerializeField] private bool _drawGizmos;
        [Space, Header("Settings")]
        [SerializeField] private GameObject _cloudPrefab;
        [SerializeField, Range(0, 1)] private float _cloudsRate = .5f;
        [SerializeField] private float _cloudsSpeed = 1f;
        [SerializeField] private float _cloudsMaxHeight;
        [SerializeField] private float _cloudsMinHeight;

        private List<GameObject> _clouds = new List<GameObject>();
        private float _cloudsLeftRange = -5f;
        private float _cloudsRightRange = 5f;

        private void Start()
        {
            GameObject spawnedCloud = Instantiate(_cloudPrefab, transform);
            spawnedCloud.transform.position = new Vector3(1, Random.Range(_cloudsMaxHeight, _cloudsMinHeight));
            _clouds.Add(spawnedCloud);
        }

        private void Update()
        {
            if (_clouds[0].transform.position.x <= _cloudsLeftRange)
            {
                Destroy(_clouds[0].gameObject);
                _clouds.RemoveAt(0);
            }

            foreach (var item in _clouds)
            {
                item.transform.position = Vector3.Lerp(item.transform.position,
                                new Vector3(item.transform.position.x - 1f, item.transform.position.y), 
                                Time.deltaTime * _cloudsSpeed);
            }

            Transform cloud = _clouds[_clouds.Count - 1].transform;
            if (cloud.position.x < _cloudsRightRange - 5 * _cloudsRate)
            {
                GameObject spawnedCloud = Instantiate(_cloudPrefab, transform);
                spawnedCloud.transform.position = new Vector3(_cloudsRightRange, Random.Range(_cloudsMaxHeight, _cloudsMinHeight));
                _clouds.Add(spawnedCloud);
            }
        }

        private void OnDrawGizmos()
        {
            if(!_drawGizmos)
                return;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(-10, _cloudsMaxHeight), new Vector3(10, _cloudsMaxHeight));
            Gizmos.DrawLine(new Vector3(-10, _cloudsMinHeight), new Vector3(10, _cloudsMinHeight));
        }
    }
}