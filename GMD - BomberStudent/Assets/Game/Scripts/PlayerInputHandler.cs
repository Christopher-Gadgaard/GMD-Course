using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [Header("Input Action Asset")] 
        [SerializeField] private InputActionAsset playerControls;

        [Header("Action Map Name Reference")] 
        [SerializeField] private string actionMapName = "Player1";
        
        [Header("Action Name Reference")]
        [SerializeField] private string move = "Move";

        private InputAction moveAction;
        
        public Vector2 MoveInput { get; private set; }
        
        private void Awake()
        {
            moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
            RegisterInputActions();
        }

        private void RegisterInputActions()
        {
            moveAction.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
            moveAction.canceled += _ => MoveInput = Vector2.zero;
        }

        private void OnEnable()
        {
            moveAction.Enable();
        }
        
        private void OnDisable()
        {
            moveAction.Disable();
        }
    }
}
