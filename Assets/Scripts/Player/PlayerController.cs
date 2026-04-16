using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    private Camera _cam;

    void Start()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // 1. Get raw input from WASD / Arrow Keys
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(horizontal, 0, vertical).normalized;
        Debug.Log(inputDir);
        if (inputDir.magnitude >= 0.1f)
        {
            // 2. Calculate the direction relative to the Camera's rotation
            // We ignore the camera's X rotation (tilt) so the player stays on the floor
            Vector3 camForward = _cam.transform.forward;
            Vector3 camRight = _cam.transform.right;

            camForward.y = 0;
            camRight.y = 0;

            camForward = camForward.normalized;
            camRight = camRight.normalized;

            // 3. Create the final movement vector
            Vector3 moveDirection = (camForward * inputDir.z) + (camRight * inputDir.x);

            // 4. Apply movement
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            // 5. Smoothly rotate the player to face the direction of movement
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}