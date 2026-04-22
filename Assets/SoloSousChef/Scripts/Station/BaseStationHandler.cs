using com.SoloSousChef.Ingredients;
using com.SoloSousChef.Interfaces;
using com.SoloSousChef.Player;
using UnityEngine;

namespace com.SoloSousChef.Station
{
    public abstract class BaseStationHandler : MonoBehaviour, IInteractable
    {
        [SerializeField] protected IngredientProcessor processor;
        [SerializeField] protected StationVisualController stationView;
        [SerializeField] protected GameObject ingredientPrefab;
        [SerializeField] protected Transform itemSocket;
        protected Ingredient _placedIngredient;

        protected virtual void OnEnable() => processor.OnProcessingComplete += HandleLogicCompletion;
        protected virtual void OnDisable() => processor.OnProcessingComplete -= HandleLogicCompletion;
        [SerializeField] protected StationType acceptedStationType;

        public virtual void OnFocus(PlayerController player)
        {
            // Place logic
            if (player.Hand.IsHandFull() && !processor.IsBusy && !processor.IsComplete)
            {
                IngredientData data = player.Hand.GetHeldItemData();
                bool processed = player.Hand.IsHeldItemProcessed();

                if (data.processingStation == acceptedStationType && !processed)
                {
                    PlaceItem(player, data);
                    return;
                }
            }

            // Auto-pickup logic
            if (processor.IsComplete && !player.Hand.IsHandFull())
            {
                PickupItem(player);
            }
        }

        public virtual void OnDefocus() { }

        public virtual void Interact(PlayerController player) { }

        protected virtual void PlaceItem(PlayerController player, IngredientData data)
        {
            player.Hand.ClearHand();
            GameObject obj = Instantiate(ingredientPrefab, itemSocket.position, Quaternion.identity, itemSocket);

            _placedIngredient = obj.GetComponent<Ingredient>();
            _placedIngredient.Initialize(data, false);
            processor.StartProcessing(data);
        }

        protected virtual void PickupItem(PlayerController player)
        {
            player.Hand.SetHeldItem(processor.GetProcessedData(), true);
            if (_placedIngredient != null) Destroy(_placedIngredient.gameObject);

            processor.Reset();
            stationView.ResetView();
        }



        protected virtual void HandleLogicCompletion()
        {
            if (_placedIngredient != null) _placedIngredient.SetProcessed();
        }
    }
}