using System;
using UnityEngine;

namespace Game.Scripts
{
  public class MovementController : MonoBehaviour
    {
        [Header("Player ID")] 
        [SerializeField] private string playerId = "Player1";
        
        
        [Header("Movement Speed")] 
        [SerializeField] internal float speed = 5f;
        public static event Action<string, float> OnSpeedChanged;
        private const float MaxSpeed = 15f;  

        [Header("Animator")] 
        [SerializeField] private Animator animator;

        private Vector2 direction;
        private Rigidbody2D myRigidBody2D;
        private PlayerInputHandler inputHandler;
        
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Death = Animator.StringToHash("Death");

        private void Start()
        {
            OnSpeedChanged?.Invoke(playerId, speed); 
        }

        private void Awake()
        {
            myRigidBody2D = GetComponent<Rigidbody2D>();
            inputHandler = GetComponent<PlayerInputHandler>();
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

        public void IncreaseSpeed()
        {
            speed = Mathf.Min(speed + 1, MaxSpeed); 
            OnSpeedChanged?.Invoke(playerId, speed); 
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
            {
                DeathSequence();
            }
        }

        private void DeathSequence()
        {
            enabled = false;
            GetComponent<BombController>().enabled = false;
            animator.Play(Death);
            AudioManager.Instance.PlaySound(AudioManager.Instance.gameOverClip);
            Invoke(nameof(OnDeathSequenceEnded), 1.25f);
        }

        private void OnDeathSequenceEnded()
        {
            gameObject.SetActive(false);
            FindObjectOfType<GameManager>().CheckWinState();
        }
    }
}