using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StationType { Refrigerator, Stove, Trash, Table, Window }

public class InteractableStation : MonoBehaviour 
{
    public StationType type;
    public static System.Action<InteractableStation> OnStationEnter;
    public static System.Action<InteractableStation> OnStationExit;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player")) 
        {
            OnStationEnter?.Invoke(this);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player")) 
        {
            OnStationExit?.Invoke(this);
        }
    }
}