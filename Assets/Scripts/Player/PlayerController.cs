using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Sub-Modules")]
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private PlayerLocomotion locomotion;
    [SerializeField] private PlayerInteractionHandler interactionHandler;
    [SerializeField] private PlayerHandController handController;
    [SerializeField] private PlayerInteractionSensor interactionSensor;

    private void OnEnable()
    {
        // Subscribe to Sensor Events
        interactionSensor.OnInteractableDetected += HandleDetection;
        interactionSensor.OnInteractableLost += HandleLost;

        // Subscribe to Input Events
        inputHandler.OnInteractPressed += HandleInteractionRequest;
    }

    private void OnDisable()
    {
        // Cleanup subscriptions to prevent memory leaks
        interactionSensor.OnInteractableDetected -= HandleDetection;
        interactionSensor.OnInteractableLost -= HandleLost;
        inputHandler.OnInteractPressed -= HandleInteractionRequest;
    }

    private void Update()
    {
        // The Orchestrator drives the Locomotion using the Input data
        // We pass the raw Vector2; Locomotion handles the speed/time math.
        locomotion.Move(inputHandler.MovementInput);
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