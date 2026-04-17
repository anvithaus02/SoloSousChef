using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerInteractionSensor interactionSensor;
    [SerializeField] private Transform ingredientAttachmentPoint;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float rotationSmoothness = 15f;
    [SerializeField] private SpriteRenderer _itemInHand;

    private Ingredient _heldIngredient;
    private IInteractable _currentInteractable;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

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

        Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            Vector3 cameraForward = _mainCamera.transform.forward;
            Vector3 cameraRight = _mainCamera.transform.right;

            cameraForward.y = 0;
            cameraRight.y = 0;

            Vector3 moveDirection = (cameraForward.normalized * inputDirection.z) + (cameraRight.normalized * inputDirection.x);
            transform.position += moveDirection.normalized * movementSpeed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
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
                fridge.CycleSelection();
            }
        }
    }

    private void SetActiveInteractable(IInteractable interactable)
    {
        _currentInteractable = interactable;
    }

    private void ClearActiveInteractable(IInteractable interactable)
    {
        if (_currentInteractable == interactable)
        {
            _currentInteractable = null;
        }
    }

    // --- Public API for Interactables to use ---

    public void SetHeldIngredient(Ingredient ingredient)
    {
        _heldIngredient = ingredient;
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