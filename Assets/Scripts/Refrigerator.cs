using UnityEngine;

public class Refrigerator : MonoBehaviour, IInteractable
{
    public void Interact(PlayerController player)
    {
        Debug.Log("Opened Fridge!");
        // Your logic for giving items goes here
    }

    public void OnFocus() { /* Show UI */ }
    public void OnDefocus() { /* Hide UI */ }
}