using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public GameObject[] players;
        [SerializeField] private GameObject playerPrefab;
        public void CheckWinState()
        {
            var aliveCount = players.Count(player => player.activeSelf);

            if (aliveCount <= 1)
            {
                Invoke(nameof(NewRound), 3f);
            }
        }

        private void NewRound()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        private void OnPlayerJoined(PlayerInput playerInput)
        {
            // Handle player joined logic
            Debug.Log($"Player {playerInput.playerIndex} joined!");
        }
    }
}
