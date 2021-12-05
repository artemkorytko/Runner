using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private const string IDLE = "Idle";
    private const string RUN = "Run";
    private const string DANCE = "Dance";
    private const string FALL = "Fall";
    
    private Animator _animator;
    private bool _isActive;
    private int _coins = 0;
    //private InputHandler _inputHandler;
    private float _moveOffset = 0;

    [SerializeField] private GameObject cameraPivot;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float roadWidth = 6;
    [SerializeField] private float rotationAngel = 10;
    [SerializeField] private float lerpSpeed = 5;

    public event Action OnFinish;
    public event Action OnDied;
    public event Action OnTakeCoin;

    public int GetCoins => _coins;
    public void StartCoins(int value) => _coins = value;

    public float SetMoveOffset
    {
        set
        {
            _moveOffset = value;
        }
    }

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

    private void Awake()
    {
        //_inputHandler = GetComponent<InputHandler>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!IsActive) return;
        Move();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            IsActive = false;
            Destroy(other.gameObject);
            Finish();
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            IsActive = false;
            Died();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            _coins += other.GetComponent<Coin>().TakeCoin;
            OnTakeCoin?.Invoke();
        }
    }

    private void Move()
    {
        //float inputX = -_inputHandler.HorizontalAxis * roadWidth;
        float inputX = -_moveOffset * roadWidth;
        Vector3 position = transform.position;
        position.x += inputX;
        position.x = Mathf.Clamp(position.x, -roadWidth * .5f, roadWidth * .5f);

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y = Mathf.LerpAngle(rotation.y, Mathf.Sin(inputX) * rotationAngel, lerpSpeed * Time.deltaTime);
        
        transform.rotation = Quaternion.Euler(rotation);
        transform.position = position;
        transform.Translate(transform.forward * speed * Time.deltaTime);
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
        StartCoroutine(RotateCamera());
    }

    private IEnumerator RotateCamera()
    {
        for (int i = 0; i < 190; i++)
        {
            yield return new WaitForFixedUpdate();
            cameraPivot?.transform.Rotate(new Vector3(0, 1, 0));
        }
    }
}
