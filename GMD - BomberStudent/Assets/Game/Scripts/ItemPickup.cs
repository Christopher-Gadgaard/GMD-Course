﻿using System;
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
        }

        public ItemType type;

        private void OnItemPickup(GameObject player)
        {
            switch (type)
            {
                case ItemType.ExtraBomb:
                    player.GetComponent<BombController>().AddBomb();
                    break;
                case ItemType.BlastRadius:
                    player.GetComponent<BombController>().explosionRadius++;
                    break;
                case ItemType.SpeedIncrease:
                    player.GetComponent<MovementController>().speed++;
                    break;
                default:
                    Debug.LogError("Could not match item pickup");
                    break;
            }
            Destroy(gameObject);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("ItemPickup");
                OnItemPickup(other.gameObject);
            }
        }
    }
}