using UnityEngine;
namespace Game.Scripts
{
    public class MovementController : MonoBehaviour
    {
        [Header("Movement Speed")] [SerializeField] private float speed = 5f;
        [Header("Animator")] [SerializeField] private Animator animator;

        private Vector2 direction;
        private Rigidbody2D myRigidBody2D;
        private PlayerInputHandler inputHandler;
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        private static readonly int Idle = Animator.StringToHash("Idle");

        private void Awake()
        {
            myRigidBody2D = GetComponent<Rigidbody2D>();
            inputHandler = PlayerInputHandler.Instance;
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
           
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            var velocity = myRigidBody2D.velocity;
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
            var moveInput = inputHandler.MoveInput;
            var movement = moveInput.normalized * speed;
            myRigidBody2D.velocity = movement;
        }
    }
}