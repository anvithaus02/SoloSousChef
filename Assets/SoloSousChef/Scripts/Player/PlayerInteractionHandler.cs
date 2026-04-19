using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour
{
    [SerializeField] private PlayerHandController handController;
    
    private IInteractable _currentInteractable;

    // These will be called by the Sensor via the Orchestrator (Controller)
    public void HandleInteractableDetected(IInteractable interactable, PlayerController player)
    {
        if (_currentInteractable != null && _currentInteractable != interactable)
        {
            _currentInteractable.OnDefocus();
        }

        _currentInteractable = interactable;
        _currentInteractable?.OnFocus(player);
    }

    public void HandleInteractableLost(IInteractable interactable)
    {
        if (_currentInteractable == interactable)
        {
            _currentInteractable.OnDefocus();
            _currentInteractable = null;
        }
    }

    // Triggered by the InputHandler
    public void PerformInteraction(PlayerController player)
    {
        _currentInteractable?.Interact(player);
    }
}