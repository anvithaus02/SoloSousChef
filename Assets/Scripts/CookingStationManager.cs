using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CookingStationManager : MonoBehaviour
{
    [System.Serializable]
    public class StationData
    {
        public bool isOccupied;
        public float currentTimer;
        public float targetTime;
        public Ingredient currentIngredient;
        public bool isDone;
    }

    public static CookingStationManager Instance { get; private set; }

    private Dictionary<CookingStationID, StationData> _registry = new Dictionary<CookingStationID, StationData>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        InitializeRegistry();
    }

    private void InitializeRegistry()
    {
        // Pre-fill the registry for every possible stove in our Enum
        foreach (CookingStationID id in System.Enum.GetValues(typeof(CookingStationID)))
        {
            _registry.Add(id, new StationData());
        }
    }
}