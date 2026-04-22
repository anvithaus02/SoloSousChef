using System;
using com.SoloSousChef.Interfaces;
using com.SoloSousChef.Player;
using UnityEngine;
namespace com.SoloSousChef.Station
{
    public class ServingTable : MonoBehaviour, IInteractable
    {
        public static event Action<bool> OnPlayerAtCounter;
        public void OnFocus(PlayerController player)
        {
            OnPlayerAtCounter?.Invoke(true);
        }
        public void OnDefocus()
        {
            OnPlayerAtCounter?.Invoke(false);
        }
        public void Interact(PlayerController player)
        {

        }
    }
}