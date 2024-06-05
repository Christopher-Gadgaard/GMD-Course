using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerInput playerInput;
        private InputAction moveAction;

        public Vector2 MoveInput { get; private set; }

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            InitializeMoveAction();
        }

        private void InitializeMoveAction()
        {
            if (playerInput == null) return;
            moveAction = playerInput.actions["Move"];
            if (moveAction == null) return;
            RegisterInputActions();
        }

        private void RegisterInputActions()
        {
            moveAction.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
            moveAction.canceled += _ => MoveInput = Vector2.zero;
        }

        private void OnEnable()
        {
            moveAction?.Enable();
        }

        private void OnDisable()
        {
            moveAction?.Disable();
        }

        public InputAction GetMoveAction()
        {
            return moveAction;
        }
    }
}