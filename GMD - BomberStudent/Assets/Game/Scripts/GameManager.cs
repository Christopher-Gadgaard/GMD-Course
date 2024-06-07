using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public GameObject[] players;
        public string mainMenuSceneName = "Main Menu";

        private void Start()
        {
            AssignPlayers();
        }

        private void AssignPlayers()
        {
            // Assuming all player objects have a common tag, like "Player"
            players = GameObject.FindGameObjectsWithTag("Player");
        }

        public void CheckWinState()
        {
            AssignPlayers(); // Ensure players are reassigned before checking win state

            var aliveCount = players.Count(player => player.activeSelf);

            if (aliveCount <= 1)
            {
                Invoke(nameof(GoToMainMenu), 3f);
            }
        }

        private void GoToMainMenu()
        {
            foreach (var player in players)
            {
                var playerInput = player.GetComponent<PlayerInput>();
                if (playerInput != null)
                {
                    playerInput.enabled = false;
                }
            }

            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}