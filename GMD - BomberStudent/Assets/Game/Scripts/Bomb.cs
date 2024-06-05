using UnityEngine;

namespace Game.Scripts
{
    public class Bomb : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Vector2 conveyorDirection;
        private float baseSpeed = 1f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (conveyorDirection != Vector2.zero)
            {
                rb.velocity = conveyorDirection * baseSpeed;
            }
        }

        public void BoostSpeed(float speedIncreasePercentage, Vector2 direction)
        {
            baseSpeed = rb.velocity.magnitude * (1 + speedIncreasePercentage / 100);
            conveyorDirection = direction.normalized;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;
            var pushDirection = collision.relativeVelocity.normalized;
            rb.velocity = pushDirection * baseSpeed;
            rb.AddForce(pushDirection * 0.5f, ForceMode2D.Impulse);
        }
    }
}