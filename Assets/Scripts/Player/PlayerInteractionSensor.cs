using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class PlayerInteractionSensor : MonoBehaviour
{
    public event Action<InteractableStation> OnStationDetected;
    public event Action<InteractableStation> OnStationLost;

    private void OnTriggerEnter(Collider other)
    {
        // The player "finds" a station
        if (other.TryGetComponent<InteractableStation>(out var station))
        {
            OnStationDetected?.Invoke(station);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // The player "leaves" the station
        if (other.TryGetComponent<InteractableStation>(out var station))
        {
            OnStationLost?.Invoke(station);
        }
    }
}