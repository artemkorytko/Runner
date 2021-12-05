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
            Destroy(effect.gameObject, 1f);
            return _price;
        }
    }

    private void Start()
    {
        effect = Instantiate(_effect, transform.parent).GetComponent<ParticleSystem>();
        effect.transform.position = transform.position;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, _rotationSpeed, 0));
    }
}
