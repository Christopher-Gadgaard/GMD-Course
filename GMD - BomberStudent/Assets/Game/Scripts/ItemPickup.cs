using System;
using UnityEngine;

namespace Game.Scripts
{
    public class ItemPickup : MonoBehaviour
    {
        public enum ItemType
        {
            ExtraBomb,
            BlastRadius,
            SpeedIncrease,
            ConveyorBelt
        }

        public ItemType type;

        private void OnItemPickup(GameObject player)
        {
            switch (type)
            {
                case ItemType.ExtraBomb:
                    var bombController = player.GetComponent<BombController>();
                    if (bombController != null)
                    {
                        bombController.AddBomb();
                    }
                    break;
                case ItemType.BlastRadius:
                    bombController = player.GetComponent<BombController>();
                    if (bombController != null)
                    {
                        bombController.AddRadius();
                    }
                    break;
                case ItemType.SpeedIncrease:
                    var movementController = player.GetComponent<MovementController>();
                    if (movementController != null)
                    {
                        movementController.IncreaseSpeed();
                    }
                    break;
                case ItemType.ConveyorBelt: 
                    var conveyorController = player.GetComponent<ConveyorController>();
                    if (conveyorController != null)
                    {
                        conveyorController.AddConveyorBelt();
                    }
                    break;
                default:
                    Debug.LogError("Unhandled item pickup type: " + type);
                    break;
            }
            AudioManager.Instance.PlaySound(AudioManager.Instance.pickupItemClip);

            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            OnItemPickup(other.gameObject);
        }
    }
}