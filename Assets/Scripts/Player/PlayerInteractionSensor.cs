using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class PlayerInteractionSensor : MonoBehaviour
{
    public event Action<IInteractable> OnInteractableDetected;
    public event Action<IInteractable> OnInteractableLost;

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponentInParent<IInteractable>();

        if (interactable != null)
        {
            interactable.OnFocus();
            OnInteractableDetected?.Invoke(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponentInParent<IInteractable>();

        if (interactable != null)
        {
            interactable.OnDefocus();
            OnInteractableLost?.Invoke(interactable);
        }
    }
}