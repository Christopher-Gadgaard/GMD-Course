using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace Game.Scripts
{
    public class BombController : MonoBehaviour
    {
        [Header("Input")] [SerializeField] private InputAction placeBombAction;

        [Header("Bomb")] [SerializeField] private GameObject bombPrefab;
        [SerializeField] private float bombFuseTime = 3f;
        [SerializeField] private int bombAmount = 1;
        private int bombsRemaining;

        [Header("Explosion")] [SerializeField] private Explosion explosionPrefab;
        [SerializeField] private LayerMask explosionLayerMask;
        [SerializeField] private float explosionDuration = 1f;
        [SerializeField] private int explosionRadius = 1;

        [Header("Destructible")] public Tilemap destructibleTiles;
        // public Destructible destructiblePrefab;

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

            GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
            bombsRemaining--;

            yield return new WaitForSeconds(bombFuseTime);

            position = bomb.transform.position;
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);

            Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            explosion.SetAnimator(explosion.startAnimator, explosion.startRenderer); 
            explosion.DestroyAfter(explosionDuration);

            Explode(position, Vector2.up, explosionRadius);
            Explode(position, Vector2.down, explosionRadius);
            Explode(position, Vector2.left, explosionRadius);
            Explode(position, Vector2.right, explosionRadius);

            Destroy(bomb);
            bombsRemaining++;
        }

        private void Explode(Vector2 position, Vector2 direction, int length)
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

            Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            if (length > 1) {
                explosion.SetAnimator(explosion.middleAnimator, explosion.middleRenderer); 
            } else {
                explosion.SetAnimator(explosion.endAnimator, explosion.endRenderer); 
            }
            explosion.SetDirection(direction);
            explosion.DestroyAfter(explosionDuration);

            Explode(position, direction, length - 1);
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
            Vector3Int cell = destructibleTiles.WorldToCell(position);
            TileBase tile = destructibleTiles.GetTile(cell);

            if (tile != null)
            {
                //  Instantiate(destructiblePrefab, position, Quaternion.identity);
                destructibleTiles.SetTile(cell, null);
            }
        }

        public void AddBomb()
        {
            bombAmount++;
            bombsRemaining++;
        }
    }
}