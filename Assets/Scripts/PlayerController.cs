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
    public event Action OnDie;

    private bool isActive;
    [SerializeField] private float moveSpeed = 10f;

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
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isActive)
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
            && collision.gameObject.layer == 6 
            && collision.gameObject.GetComponent<Wall>())
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Finish"))
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

}
