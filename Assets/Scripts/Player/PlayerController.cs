using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerInteractionSensor interactionSensor;
    [SerializeField] private Transform ingredientAttachmentPoint;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float rotationSmoothness = 15f;

    private Ingredient _heldIngredient;
    private IInteractable _currentInteractable;

    private void OnEnable()
    {
        interactionSensor.OnInteractableDetected += SetActiveInteractable;
        interactionSensor.OnInteractableLost += ClearActiveInteractable;
    }

    private void OnDisable()
    {
        interactionSensor.OnInteractableDetected -= SetActiveInteractable;
        interactionSensor.OnInteractableLost -= ClearActiveInteractable;
    }

    private void Update()
    {
        ProcessMovement();
        ProcessInteractionInput();
        ProcessSelectionInput();
    }

    private void ProcessMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Use Y for vertical movement to move UP the screen background
        Vector3 moveDirection = new Vector3(horizontal, vertical, 0).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            transform.position += moveDirection * movementSpeed * Time.deltaTime;

            // Rotation logic adjusted for 2D/2.5D plane
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothness * Time.deltaTime);
        }
    }

    private void ProcessInteractionInput()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            if (_currentInteractable != null)
            {
                _currentInteractable.Interact(this);
            }
        }
    }

    private void ProcessSelectionInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_currentInteractable is Refrigerator fridge)
            {
               //fridge.CycleSelection();
            }
        }
    }

    private void SetActiveInteractable(IInteractable interactable)
    {
        _currentInteractable = interactable;
        Debug.Log("Trigger : ");
    }

    private void ClearActiveInteractable(IInteractable interactable)
    {
        if (_currentInteractable == interactable)
        {
            _currentInteractable = null;
        }
    }

    public void SetHeldIngredient(Ingredient ingredient)
    {
        _heldIngredient = ingredient;
        _heldIngredient.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 10;
        _heldIngredient.transform.SetParent(ingredientAttachmentPoint);
        _heldIngredient.transform.localPosition = Vector3.zero;
        _heldIngredient.transform.localRotation = Quaternion.identity;
    }

    public Ingredient GetHeldIngredient()
    {
        return _heldIngredient;
    }

    public Ingredient ReleaseHeldIngredient()
    {
        Ingredient releasedItem = _heldIngredient;
        _heldIngredient = null;

        if (releasedItem != null)
        {
            releasedItem.transform.SetParent(null);
        }

        return releasedItem;
    }

    public void DestroyHeldIngredient()
    {
        if (_heldIngredient != null)
        {
            GameObject objectToDestroy = _heldIngredient.gameObject;
            _heldIngredient = null;
            Destroy(objectToDestroy);
        }
    }
}