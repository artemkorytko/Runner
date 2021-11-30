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

    private Animator animator;

    public event Action OnFinish;
    public event Action OnDied;

    private bool isActive; // = isAlive
    [SerializeField] private float moveSpeed = 10;
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
    }
    void Update()
    {
        if (!IsActive)
            return;

        Move();
    }

    private void Move()
    {
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall") 
            || collision.gameObject.layer == 6 
            || collision.gameObject.GetComponent<Wall>())
        {
            //    Debug.Log(collision.contacts[0].point)
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
        isActive = false;
        OnDied?.Invoke();
    }
    private void Finish()
    {
        animator.SetTrigger(DANCE);
        OnFinish?.Invoke();
    }
}
