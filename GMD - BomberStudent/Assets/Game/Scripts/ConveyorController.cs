using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts
{
    public class ConveyorController : MonoBehaviour
    {
        public GameObject conveyorBeltPrefab;
        public static event Action<string, int> OnConveyorsChanged;

        [Header("Input")]
        private InputAction placeConveyorAction;
        private InputAction moveAction;

        private const int MaxConveyors = 5;
        private int conveyorBeltCount = 1;
        [SerializeField] private string playerId = "Player1";

        private void Awake()
        {
            var playerInput = GetComponent<PlayerInput>();
            placeConveyorAction = playerInput.actions["ConveyorBelt"];
            placeConveyorAction.performed += _ => PlaceConveyorBelt();

            InitializeMoveAction(playerInput);
        }

        private void InitializeMoveAction(PlayerInput playerInput)
        {
            moveAction = playerInput.actions["Move"];
            moveAction?.Enable();
        }

        private void OnEnable()
        {
            placeConveyorAction.Enable();
        }

        private void OnDisable()
        {
            placeConveyorAction.Disable();
        }

        private void Start()
        {
            OnConveyorsChanged?.Invoke(playerId, conveyorBeltCount);
        }

        public void AddConveyorBelt()
        {
            if (conveyorBeltCount >= MaxConveyors) return;
            conveyorBeltCount++;
            OnConveyorsChanged?.Invoke(playerId, conveyorBeltCount);
        }

        private void PlaceConveyorBelt()
        {
            if (conveyorBeltCount <= 0)
            {
                return;
            }

            if (moveAction == null)
            {
                return;
            }

            Vector2 position = transform.position;
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);

            var conveyorBelt = Instantiate(conveyorBeltPrefab, position, Quaternion.identity);
            var conveyorBeltScript = conveyorBelt.GetComponent<ConveyorBelt>();
            if (conveyorBeltScript != null)
            {
                var playerDirection = moveAction.ReadValue<Vector2>();
                conveyorBeltScript.InitializeDirection(playerDirection);
            }

            conveyorBeltCount--;
            OnConveyorsChanged?.Invoke(playerId, conveyorBeltCount);
        }
    }
}
