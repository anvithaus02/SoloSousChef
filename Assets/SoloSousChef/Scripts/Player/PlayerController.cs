using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [Header("Sub-Modules")]
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private PlayerLocomotion locomotion;
    [SerializeField] private PlayerInteractionHandler interactionHandler;
    [SerializeField] private PlayerHandController handController;
    [SerializeField] private PlayerInteractionSensor interactionSensor;

    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        interactionSensor.OnInteractableDetected += HandleDetection;
        interactionSensor.OnInteractableLost += HandleLost;

        inputHandler.OnInteractPressed += HandleInteractionRequest;

    }

    private void OnDisable()
    {
        interactionSensor.OnInteractableDetected -= HandleDetection;
        interactionSensor.OnInteractableLost -= HandleLost;
        inputHandler.OnInteractPressed -= HandleInteractionRequest;

    }

    private void Update()
    {
        locomotion.Move(inputHandler.MovementInput);
    }

    public void SetPlayerDisplayState(bool isVisible)
    {
        playerObject.SetActive(isVisible);
    }

    private void HandleDetection(IInteractable interactable)
    {
        // Delegate the logic to the Interaction Handler
        interactionHandler.HandleInteractableDetected(interactable, this);
    }

    private void HandleLost(IInteractable interactable)
    {
        interactionHandler.HandleInteractableLost(interactable);
    }

    private void HandleInteractionRequest()
    {
        // When the InputHandler detects 'E', we tell the Interaction system to execute
        interactionHandler.PerformInteraction(this);
    }

    // Public property to give other scripts (like Fridge) access to the Hand
    public PlayerHandController Hand => handController;
}