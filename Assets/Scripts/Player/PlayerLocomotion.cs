using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float rotationSmoothness = 15f;

    public void Move(Vector2 direction)
    {
        if (direction.magnitude < 0.1f) return;

        Vector3 moveDir = new Vector3(direction.x, direction.y, 0);
        transform.position += moveDir * movementSpeed * Time.deltaTime;

        ApplyRotation(direction);
    }

    private void ApplyRotation(Vector2 direction)
    {
        // Keeping your original 2D/2.5D plane rotation logic
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothness * Time.deltaTime);
    }
}