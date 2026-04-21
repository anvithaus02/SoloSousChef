using System;
using UnityEngine;

public class ServingTable : MonoBehaviour, IInteractable
{
    // This event tells the UI "The player is standing in front of the counter"
    public static event Action<bool> OnPlayerAtCounter;

    public void OnFocus(PlayerController player)
    {
        // When the player looks at the table, send 'true' to enable Serve buttons
        OnPlayerAtCounter?.Invoke(true);
    }

    public void OnDefocus()
    {
        // When the player looks away, send 'false' to disable Serve buttons
        OnPlayerAtCounter?.Invoke(false);
    }

    public void Interact(PlayerController player)
    {
        
    }
}