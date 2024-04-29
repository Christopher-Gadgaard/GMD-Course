using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Scripts
{
    public class GameUIController : MonoBehaviour
    {
        private VisualElement root;
        private ProgressBar player1SpeedProgressBar;
        private ProgressBar player1BombProgressBar;
        private ProgressBar player1RadiusProgressBar;

        private void Start()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            player1SpeedProgressBar = root.Q<ProgressBar>("player1-speed-bar");
            player1BombProgressBar = root.Q<ProgressBar>("player1-bomb-bar");
            player1RadiusProgressBar = root.Q<ProgressBar>("player1-radius-bar");
        }

        private void OnEnable()
        {
            MovementController.OnSpeedChanged += UpdatePlayerSpeed;
            BombController.OnBombsChanged += UpdatePlayerBombs;
            BombController.OnRadiusChanged += UpdatePlayerRadius;
        }

        private void OnDisable()
        {
            MovementController.OnSpeedChanged -= UpdatePlayerSpeed;
            BombController.OnBombsChanged -= UpdatePlayerBombs;
            BombController.OnRadiusChanged -= UpdatePlayerRadius;
        }

        private void UpdatePlayerSpeed(string playerId, float speed)
        {
            if (playerId == "Player1")
            {
                var normalizedSpeed = speed / 15; 
                player1SpeedProgressBar.value = normalizedSpeed * 100;  
            }
        }

        private void UpdatePlayerBombs(string playerId, int bombs)
        {
            if (playerId == "Player1")
            {
                var normalizedBombs = (float)bombs / 10; 
                player1BombProgressBar.value = normalizedBombs * 100;  
            }
        }

        private void UpdatePlayerRadius(string playerId, int radius)
        {
            if (playerId == "Player1")
            {
                var normalizedRadius = (float)radius / 10;  
                player1RadiusProgressBar.value = normalizedRadius * 100;  
            }
        }
    }
}
