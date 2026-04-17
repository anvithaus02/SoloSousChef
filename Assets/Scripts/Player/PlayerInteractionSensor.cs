using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class PlayerInteractionSensor : MonoBehaviour
{
    public event Action<IInteractable> OnInteractableDetected;
    public event Action<IInteractable> OnInteractableLost;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            interactable.OnFocus(); 
            OnInteractableDetected?.Invoke(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            interactable.OnDefocus(); 
            OnInteractableLost?.Invoke(interactable);
        }
    }
}