using UnityEngine;

namespace Game.Scripts
{
    public class ConveyorBelt : MonoBehaviour
    {
        [SerializeField] private float speedIncreasePercentage = 20f; 
        private Vector2 direction;

        public void InitializeDirection(Vector2 playerDirection)
        {
            direction = playerDirection == Vector2.zero ? Vector2.down : playerDirection.normalized;
            RotateConveyor(direction);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Bomb")) return;
            var bomb = other.GetComponent<Bomb>();
            if (bomb != null)
            {
                bomb.BoostSpeed(speedIncreasePercentage, direction);
            }
        }

        private void RotateConveyor(Vector2 vector2)
        {
            var angle = 0f;
            if (vector2 == Vector2.up)
                angle = 90f;
            else if (vector2 == Vector2.right)
                angle = 0f;
            else if (vector2 == Vector2.down)
                angle = 270f;
            else if (vector2 == Vector2.left)
                angle = 180f;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}