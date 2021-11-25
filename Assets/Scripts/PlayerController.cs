using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private const string IDLE = "Idle";
    private const string RUN = "Run";
    private const string DANCE = "Dance";
    private const string FALL = "Fall";
    
    private Animator _animator;
    private bool _isActive;

    [SerializeField] private float _speed = .2f;

    public event Action OnFinish;
    public event Action OnDied;

    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            if (value == true)
                _animator.SetTrigger(RUN);
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        IsActive = true;
    }
    private void Update()
    {
        if (!IsActive) return;
        Move();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            IsActive = false;
            Finish();
        }
    }

    private void Move()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + _speed * Time.deltaTime);
    }

    private void Died()
    {
        _animator.SetTrigger(FALL);
        OnDied?.Invoke();
    }

    private void Finish()
    {
        _animator.SetTrigger(DANCE);
        OnFinish?.Invoke();
    }
}
