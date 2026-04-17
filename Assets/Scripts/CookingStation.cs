using UnityEngine;

public class CookingStation : MonoBehaviour, IInteractable
{
    [Header("Station Identity")]
    [SerializeField] private CookingStationID stationID; 
    
    public CookingStationID ID => stationID;

    public void OnFocus(PlayerController playerController)
    {
        Debug.Log($"<color=orange>Cooking Station:</color> Over {stationID}.");
    }

    public void OnDefocus()
    {
        Debug.Log($"<color=gray>Cooking Station:</color> Left {stationID}.");
    }

    public void Interact(PlayerController player)
    {
        Debug.Log($"<color=orange>Cooking Station:</color> Interacting with {stationID}.");
        
        // Logic check: StoveManager.Instance.HandleStationInteraction(this, player);
    }
}