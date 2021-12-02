using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _price = 1;
    [SerializeField] private float _rotationSpeed = 2f;
    [SerializeField] private ParticleSystem _effect;

    public int TakeCoin
    {
        get
        {
            _effect.Play();
            Destroy(gameObject);
            return _price;
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, _rotationSpeed, 0));
    }
}
