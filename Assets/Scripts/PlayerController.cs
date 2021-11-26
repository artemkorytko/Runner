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
    public bool IsActive
    {
        get => IsActive;
        set 
        {
            if(value == true)
            {
                animator.SetTrigger(RUN);
            }
            isActive = value;
        }
    }
    void Start()
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
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
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
