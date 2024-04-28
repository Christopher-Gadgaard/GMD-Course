using UnityEngine;

namespace Game.Scripts
{
    public class Destructible : MonoBehaviour
    {
        [SerializeField] private float destructionTime = 0.3f;
    
        [Range(0f,1f)]
        [SerializeField] private float itemSpawnChance = 0.2f;
        [SerializeField] private GameObject[] spawnAbleItems;
        private void Start()
        {
            Destroy(gameObject,destructionTime);
        }

        private void OnDestroy()
        {
            if (spawnAbleItems.Length <= 0 || !(Random.value < itemSpawnChance)) return;
            var randomIndex = Random.Range(0, spawnAbleItems.Length);
            Instantiate(spawnAbleItems[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
