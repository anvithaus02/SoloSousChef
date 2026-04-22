using com.SoloSousChef.Interfaces;
using UnityEngine;
namespace com.SoloSousChef.Player
{
    public class PlayerInteractionHandler : MonoBehaviour
    {
        [SerializeField] private PlayerHandController handController;

        private IInteractable _currentInteractable;
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
        public void PerformInteraction(PlayerController player)
        {
            _currentInteractable?.Interact(player);
        }
    }
}