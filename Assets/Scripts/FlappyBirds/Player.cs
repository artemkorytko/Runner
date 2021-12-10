using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }

        private Rigidbody2D rigidbody;
        [SerializeField] private float upSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float minSpeed;

        private bool isActive;



        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            rigidbody = GetComponent<Rigidbody2D>();

        }
        private void Update()
        {
            if (!isActive)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                rigidbody.velocity += new Vector2(0, upSpeed);
            }
        }
        private void FixedUpdate()
        {
            Vector2 velocity = rigidbody.velocity;
            velocity.y = Mathf.Clamp(velocity.y, minSpeed, maxSpeed);
        }
        public void SetActive(bool isActive)
        {
            this.isActive = isActive;
            rigidbody.simulated = isActive;
        }
    }

}
