using UnityEngine;
using System.Collections;

public class TrashCan : MonoBehaviour, IInteractable
{
    public void OnFocus(PlayerController playerController)
    {
        // This fires when the PlayerInteractionSensor first detects the 2D Trigger
        Debug.Log("<color=yellow>Trash Can:</color> Player is in range.");
    }

    public void OnDefocus()
    {
        // This fires when the player walks away
        Debug.Log("<color=yellow>Trash Can:</color> Player left range.");
    }

    public void Interact(PlayerController player)
    {
        if (player.Hand.GetHeldItem() != null)
        {
            Debug.Log("<color=red>Trash Can:</color> Deleting item: " + player.Hand.GetHeldItem().name);
            player.Hand.DestroyHeldItem();
        }
        else
        {
            Debug.Log("<color=white>Trash Can:</color> Interaction pressed, but hand is empty.");
        }
    }
}