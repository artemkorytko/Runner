using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string IDLE = "Idle";
    private const string RUN = "Run";
    private const string FALL = "Fall";
    private const string DANCE = "Dance";

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float lerpSpeed = 30;
    [SerializeField] private float rotationAngle = 20;
    private float roadWidth;

    private Animator animator;
    public event Action OnFinish;
    public event Action OnDie;

    private bool isActive;
    private float horizontal = 0f;
    private InputHandler inputHandler;
    private Transform viewModel;
    private float playerPosZ;
    public float PlayerPosZ
    {
        get => playerPosZ;
    }

    public bool IsActive
    {
        get => isActive;
        set
        {
            if (value == true)
            {
                animator.SetTrigger(RUN);
            }
            isActive = value;
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputHandler = GetComponent<InputHandler>();
        viewModel = transform.GetChild(0);
    }

    void Update()
    {
        if (!isActive)
            return;

        Move();
    }

    private void Move()
    {
        float offsetX = inputHandler.HorizontalAxis * roadWidth;
        Vector3 position = transform.localPosition;
        position.x += offsetX;
        position.x = Mathf.Clamp(position.x, -roadWidth * 0.5f, roadWidth * 0.5f);

        if(offsetX != 0)
        {
            Vector3 rotation = viewModel.localRotation.eulerAngles;
            rotation.y = Mathf.LerpAngle(rotation.y, Mathf.Sign(offsetX) * rotationAngle, lerpSpeed * Time.deltaTime);
            viewModel.localRotation = Quaternion.Euler(rotation);
        }
       
        transform.localPosition = position;
        // horizontal = Input.GetAxis("Horizontal");
        // transform.Translate(horizontal * transform.right * moveSpeed * Time.deltaTime);
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime);

        playerPosZ = position.z;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            Finish();
        }
    }
    private void Die()
    {
        animator.SetTrigger(FALL);
        isActive = false;
        OnDie?.Invoke();
    }
    private void Finish()
    {
        animator.SetTrigger(DANCE);
        isActive = false;
        OnFinish?.Invoke();
    }

    public void SetupRoadWidth(float value)
    {
        roadWidth = value;
    }
}
