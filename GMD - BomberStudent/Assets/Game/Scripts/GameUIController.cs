using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Scripts
{
    public class GameUIController : MonoBehaviour
    {
        private VisualElement root;
        private ProgressBar player1SpeedProgressBar;
        private ProgressBar player2SpeedProgressBar;
        private ProgressBar player1BombProgressBar;
        private ProgressBar player2BombProgressBar;
        private ProgressBar player1RadiusProgressBar;
        private ProgressBar player2RadiusProgressBar;

        private void Start()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            player1SpeedProgressBar = root.Q<ProgressBar>("player1-speed-bar");
            player2SpeedProgressBar = root.Q<ProgressBar>("player2-speed-bar");
            player1BombProgressBar = root.Q<ProgressBar>("player1-bomb-bar");
            player2BombProgressBar = root.Q<ProgressBar>("player2-bomb-bar");
            player1RadiusProgressBar = root.Q<ProgressBar>("player1-radius-bar");
            player2RadiusProgressBar = root.Q<ProgressBar>("player2-radius-bar");
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
            var normalizedSpeed = speed / 15; 
            if (playerId == "Player1")
            {
                player1SpeedProgressBar.value = normalizedSpeed * 100;  
            }
            else if(playerId == "Player2")
            {
                player2SpeedProgressBar.value = normalizedSpeed * 100;
            }
        }

        private void UpdatePlayerBombs(string playerId, int bombs)
        {
            var normalizedBombs = (float)bombs / 10; 
            if (playerId == "Player1")
            {
                player1BombProgressBar.value = normalizedBombs * 100;  
            }
            else if(playerId == "Player2")
            {
                player2BombProgressBar.value = normalizedBombs * 100;
            }
        }

        private void UpdatePlayerRadius(string playerId, int radius)
        {
            var normalizedRadius = (float)radius / 10;  
            if (playerId == "Player1")
            {
                player1RadiusProgressBar.value = normalizedRadius * 100;  
            }
            else if(playerId == "Player2")
            {
                player2RadiusProgressBar.value = normalizedRadius * 100;
            }
        }
    }
}
