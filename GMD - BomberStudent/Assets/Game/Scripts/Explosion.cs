using System;
using UnityEngine;

namespace Game.Scripts
{
    public class Explosion : MonoBehaviour
    {
        [Header("Animator")] [SerializeField] private Animator animator;

        private static readonly int End = Animator.StringToHash("ExplosionEnd");
        private static readonly int Middle = Animator.StringToHash("ExplosionMiddle");
        private static readonly int Start = Animator.StringToHash("ExplosionStart");
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void PlayEndAnimation()
        {
            animator.Play(End);
        }
        
        public void PlayMiddleAnimation()
        {
            animator.Play(Middle);
        }

        public void PlayStartAnimation()
        {
            animator.Play(Start);
        }

        public void SetDirection(Vector2 direction)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        public void DestroyAfter(float seconds)
        {
            Destroy(gameObject, seconds);
        }
    }
}
