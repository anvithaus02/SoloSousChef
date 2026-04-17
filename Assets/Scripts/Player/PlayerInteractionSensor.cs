using UnityEngine;
using System;

public class PlayerInteractionSensor : MonoBehaviour
{
    public event Action<IInteractable> OnInteractableDetected;
    public event Action<IInteractable> OnInteractableLost;

    // We use 2D here because the player is moving on the XY plane
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Physics2D Trigger detected something: " + other.name);
        IInteractable interactable = other.GetComponentInChildren<IInteractable>();
        if (interactable != null)
        {
            OnInteractableDetected?.Invoke(interactable);
                    Debug.Log("Physics2D Invoked " + other.name);

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