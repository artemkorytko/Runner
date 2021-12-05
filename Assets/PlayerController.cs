using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string IDLE = "Idle";
    private const string RUN = "Run";
    private const string FALL = "Fall";
    private const string DANCE = "Dance";

    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float lerpSpeed = 30;
    [SerializeField] private float rotationAngle = 10;
    private float roadWidth;
    

    private Animator animator;
    private InputHandler inputHandler;
    private Transform viewModel;

    public event Action OnFinish;
    public event Action OnDied;

    private bool isActive;
   

    public bool IsActive
    {
        get => isActive;

        set
        {
            if(value == true)
            {
                animator.SetTrigger(RUN);
            }
            isActive = value;
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        inputHandler = GetComponent<InputHandler>();
        viewModel = transform.GetChild(0);
    }

    void Update()
    {
        if (!IsActive)
            return;

        Move();
    }

    private void Move()
    {
        float offsetX = inputHandler.HorizontalAxis * roadWidth;
        Vector3 position = transform.localPosition;
        position.x += offsetX;
        position.x = Mathf.Clamp(position.x, -roadWidth * 0.5f, roadWidth * 0.5f);

        Vector3 rotation = viewModel.localRotation.eulerAngles;
        rotation.y = Mathf.LerpAngle(rotation.y, Mathf.Sign(offsetX) * rotationAngle, lerpSpeed * Time.deltaTime);
        viewModel.localRotation = Quaternion.Euler(rotation);

        transform.localPosition =position;
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
    }   

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.CompareTag("Wall"))
        {
           Died();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Finish"))
        {
            Finish();
        }
    }

    private void Died()
    {
        animator.SetTrigger(FALL);
        IsActive = false;
        OnDied?.Invoke();
    }

    private void Finish()
    {
        animator.SetTrigger(DANCE);
        IsActive = false;
        OnFinish?.Invoke();
    }

    public void SetupRoadWidth(float value)
    {
        roadWidth = value;
    }
}
