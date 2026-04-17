using UnityEngine;

public class TrashCan : MonoBehaviour, IInteractable
{
    public void OnFocus(PlayerController player) { }
    public void OnDefocus() { }

    public void Interact(PlayerController player)
    {
        if (player.Hand.IsHandFull())
        {
            player.Hand.ClearHand();
        }
    }
}