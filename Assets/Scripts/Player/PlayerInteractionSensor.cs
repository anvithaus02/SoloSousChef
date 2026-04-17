using UnityEngine;
using System;

public class PlayerInteractionSensor : MonoBehaviour
{
    public event Action<IInteractable> OnInteractableDetected;
    public event Action<IInteractable> OnInteractableLost;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponentInParent<IInteractable>();
        if (interactable != null)
        {
            OnInteractableDetected?.Invoke(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponentInChildren<IInteractable>();
        if (interactable != null)
        {
            OnInteractableLost?.Invoke(interactable);
        }
    }
}