using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts
{
    public class PlayerSetup : MonoBehaviour
    {
        private PlayerInput playerInput;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            ConfigurePlayerInput();
        }

        private void ConfigurePlayerInput()
        {
            if (playerInput == null) return;

            switch (playerInput.playerIndex)
            {
                case 0:
                    playerInput.SwitchCurrentControlScheme("Gamepad", Gamepad.all[0]);
                    playerInput.defaultActionMap = "Player1";
                    break;
                case 1:
                    playerInput.SwitchCurrentControlScheme("Gamepad", Gamepad.all[1]);
                    playerInput.defaultActionMap = "Player2";
                    break;
                default:
                    Debug.LogError("Unhandled player index");
                    break;
            }
        }
    }
}