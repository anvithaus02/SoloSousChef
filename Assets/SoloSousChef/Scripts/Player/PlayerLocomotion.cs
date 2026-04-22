using UnityEngine;

namespace com.SoloSousChef.Player
{
    public class PlayerLocomotion : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 7f;
        public void Move(Vector2 direction)
        {
            if (direction.magnitude < 0.1f)
                return;

            Vector3 moveDir = new Vector3(direction.x, direction.y, 0);
            transform.position += moveDir * movementSpeed * Time.deltaTime;

            HandleScaleFlip(direction.x);
        }

        private void HandleScaleFlip(float horizontalInput)
        {
            if (Mathf.Abs(horizontalInput) > 0.1f)
            {
                float scaleX = horizontalInput > 0 ? 1f : -1f;
                transform.localScale = new Vector3(scaleX, 1f, 1f);
            }
        }
    }
}