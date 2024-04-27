using UnityEngine;

namespace Game.Scripts
{
    public class Explosion : MonoBehaviour
    {
        public Animator endAnimator;
        public Animator middleAnimator;
        public Animator startAnimator;
        public SpriteRenderer endRenderer; 
        public SpriteRenderer middleRenderer; 
        public SpriteRenderer startRenderer; 



        public void SetAnimator(Animator animator, SpriteRenderer spriteRenderer)
        {
            startAnimator.enabled = animator == startAnimator;
            startRenderer.enabled = spriteRenderer == startRenderer;
            middleAnimator.enabled = animator == middleAnimator;
            middleRenderer.enabled = spriteRenderer == middleRenderer;
            endAnimator.enabled = animator == endAnimator;
            endRenderer.enabled = spriteRenderer == endRenderer;
        }

        public void SetDirection(Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        public void DestroyAfter(float seconds)
        {
            Destroy(gameObject, seconds);
        }
    }
}
