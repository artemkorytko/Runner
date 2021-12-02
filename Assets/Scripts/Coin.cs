using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _price = 1;
    [SerializeField] private float _rotationSpeed = 2f;
    [SerializeField] private GameObject _effect;

    private ParticleSystem effect;

    public int TakeCoin
    {
        get
        {
            effect.Play();
            Destroy(gameObject);
            return _price;
        }
    }

    private void Start()
    {
        effect = Instantiate(_effect).GetComponent<ParticleSystem>();
        effect.transform.position = transform.position;
        Destroy(effect.gameObject, 10f);
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, _rotationSpeed, 0));
    }
}
