using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public GameObject[] players;

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
    }
}
