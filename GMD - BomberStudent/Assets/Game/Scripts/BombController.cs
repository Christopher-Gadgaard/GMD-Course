using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace Game.Scripts
{
    public class BombController : MonoBehaviour
    {
        [Header("Player ID")] 
        [SerializeField] private string playerId = "Player1";
        
        [Header("Input")] 
        [SerializeField] private InputAction placeBombAction;

        [Header("Bomb")] 
        [SerializeField] private GameObject bombPrefab;
        [SerializeField] private float bombFuseTime = 3f;
        [SerializeField] private int bombAmount = 1;
        private const int MaxBombs = 10;  
        private int bombsRemaining;
        public static event Action<string, int> OnBombsChanged;

        [Header("Explosion")] 
        [SerializeField] private Explosion explosionPrefab;
        [SerializeField] private LayerMask explosionLayerMask;
        [SerializeField] private float explosionDuration = 0.5f;
        [SerializeField] internal int explosionRadius = 1;
        private const int MaxRadius = 10;
        public static event Action<string, int> OnRadiusChanged;
        
        [Header("Destructible")] 
        [SerializeField] private Tilemap destructibleTiles;
        [SerializeField] private Destructible destructiblePrefab;

        private void OnEnable()
        {
            bombsRemaining = bombAmount;
            placeBombAction.Enable();
        }

        private void OnDisable()
        {
            placeBombAction.Disable();
        }

        private void Awake()
        {
            placeBombAction.performed += _ => PlaceBomb();
        }

        private void Start()
        {
            OnBombsChanged?.Invoke(playerId, bombAmount); 
            OnRadiusChanged?.Invoke(playerId,explosionRadius);
        }

        private void PlaceBomb()
        {
            if (bombsRemaining > 0)
            {
                StartCoroutine(PlaceBombRoutine());
            }
        }

        private IEnumerator PlaceBombRoutine()
        {
            Vector2 position = transform.position;
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);

            var bomb = Instantiate(bombPrefab, position, Quaternion.identity);
            bombsRemaining--;

            yield return new WaitForSeconds(bombFuseTime);

            position = bomb.transform.position;
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);

            var explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            explosion.PlayStartAnimation();
            explosion.DestroyAfter(explosionDuration);
            
            AudioManager.Instance.PlaySound(AudioManager.Instance.bombExplodeClip);

            Explode(position, Vector2.up, explosionRadius);
            Explode(position, Vector2.down, explosionRadius);
            Explode(position, Vector2.left, explosionRadius);
            Explode(position, Vector2.right, explosionRadius);

            Destroy(bomb);
            bombsRemaining++;
        }

        private void Explode(Vector2 position, Vector2 direction, int length)
        {
            while (true)
            {
                if (length <= 0)
                {
                    return;
                }

                position += direction;

                if (Physics2D.OverlapBox(position, new Vector2(0.5f, 0.5f), 0f, explosionLayerMask))
                {
                    ClearDestructible(position);
                    return;
                }

                var explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
                if (length > 1)
                {
                    explosion.PlayMiddleAnimation();
                }
                else
                {
                    explosion.PlayEndAnimation();
                }

                explosion.SetDirection(direction);
                explosion.DestroyAfter(explosionDuration);

                length -= 1;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer($"Bomb"))
            {
                other.isTrigger = false;
            }
        }

        private void ClearDestructible(Vector2 position)
        {
            var cell = destructibleTiles.WorldToCell(position);
            var tile = destructibleTiles.GetTile(cell);

            if (tile == null) return;
            Instantiate(destructiblePrefab, position, Quaternion.identity);
            destructibleTiles.SetTile(cell, null);
        }

        public void AddBomb()
        {
           // Debug.Log("Bomb controller added bomb");
            bombAmount = Mathf.Min(bombAmount + 1, MaxBombs); 
            bombsRemaining = Mathf.Min(bombsRemaining + 1, MaxBombs); 
            OnBombsChanged?.Invoke(playerId, bombAmount); 
        }

        public void AddRadius()
        {
           // Debug.Log("Bomb controller added Radius");
            explosionRadius = Mathf.Min(explosionRadius + 1, MaxRadius); 
            OnRadiusChanged?.Invoke(playerId,explosionRadius);
        }
    }
}