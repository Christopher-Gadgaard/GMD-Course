using UnityEngine;
namespace Game.Scripts
{
    public class MovementController : MonoBehaviour
    {
        [Header("Movement Speed")] [SerializeField] private float speed = 5f;
        [Header("Animator")] [SerializeField] private Animator animator;

        private Vector2 _direction;
        private Rigidbody2D _rigidbody2D;
        private PlayerInputHandler _inputHandler;
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        private static readonly int Idle = Animator.StringToHash("Idle");

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _inputHandler = PlayerInputHandler.Instance;
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
           
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            var velocity = _rigidbody2D.velocity;
            var isMoving = velocity.magnitude > 0.1f;
            animator.SetBool(Idle, !isMoving);
            if (isMoving)
            {
                animator.SetFloat(MoveX, velocity.x);
                animator.SetFloat(MoveY, velocity.y);
            }
            else
            {
                animator.SetFloat(MoveX, 0);
                animator.SetFloat(MoveY, 0);
            }
        }

        private void FixedUpdate()
        {
            Vector2 moveInput = _inputHandler.MoveInput;
            Vector2 movement = moveInput.normalized * speed; // Use normalized to ensure consistent movement speed in all directions
            _rigidbody2D.velocity = movement;
        }
    }
}