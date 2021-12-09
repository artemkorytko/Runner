using UnityEngine;

namespace FlapyBird
{
    public class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }
        
        [SerializeField] private float upSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float minSpeed;
        
        private Rigidbody2D _rigidbody;
        private bool _isActive;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if(!_isActive)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                _rigidbody.velocity += new Vector2(0, upSpeed);
            }
        }

        private void FixedUpdate()
        {
            Vector2 velocity = _rigidbody.velocity;
            velocity.y = Mathf.Clamp(velocity.y, minSpeed, maxSpeed);
            _rigidbody.velocity = velocity;
        }

        public void SetActive(bool isActive)
        {
            _isActive = isActive;
            _rigidbody.simulated = isActive;
        }
    }
}

