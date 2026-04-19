// using UnityEngine;

// public class ProcessingStation : MonoBehaviour, IInteractable
// {
//     [SerializeField] private StationType requiredStationType;
    
//     private Ingredient _slotIngredient;
//     private float _processTimer;
//     private bool _isProcessing;

//     public void Interact(PlayerController player)
//     {
//         Ingredient playerItem = player.GetHeldIngredient();

//         // Case 1: Station is empty, Player is holding something to process
//         if (_slotIngredient == null && playerItem != null)
//         {
//             if (playerItem.Data.processingStation == requiredStationType && !playerItem.IsProcessed)
//             {
//                 _slotIngredient = player.ReleaseHeldIngredient();
//                 _slotIngredient.transform.SetParent(transform);
//                 _slotIngredient.transform.localPosition = Vector3.up; // Visual placement
                
//                 StartProcessing();
//             }
//         }
//         // Case 2: Station has finished processing, Player takes it
//         else if (_slotIngredient != null && !_isProcessing && playerItem == null)
//         {
//             player.SetHeldIngredient(_slotIngredient);
//             _slotIngredient = null;
//         }
//     }

//     private void StartProcessing()
//     {
//         _isProcessing = true;
//         _processTimer = _slotIngredient.Data.processingTime;
//     }

//     private void Update()
//     {
//         if (!_isProcessing || _slotIngredient == null) return;

//         _processTimer -= Time.deltaTime;

//         if (_processTimer <= 0)
//         {
//             _isProcessing = false;
//             _slotIngredient.SetProcessed();
//         }
//     }
// }