using UnityEngine;

public class ChoppingStationHandler : MonoBehaviour, IInteractable
{
    [SerializeField] private IngredientProcessor processor;
    [SerializeField] private ChoppingStationView stationView;
    [SerializeField] private Transform itemSocket;
    [SerializeField] private GameObject ingredientPrefab;

    private Ingredient _placedIngredient;

    private void OnEnable()
    {
        processor.OnProcessingComplete += HandleLogicCompletion;
    }

    private void OnDisable()
    {
        processor.OnProcessingComplete -= HandleLogicCompletion;
    }
    public void OnFocus(PlayerController player)
    {
        if (player.Hand.IsHandFull() && !processor.IsBusy && !processor.IsComplete)
        {
            IngredientData data = player.Hand.GetHeldItemData();
            bool processed = player.Hand.IsHeldItemProcessed();

            if (data.processingStation == StationType.Table && !processed)
            {
                PlaceItem(player, data);
                return;
            }
        }

        if (processor.IsComplete && !player.Hand.IsHandFull())
        {
            PickupItem(player);
        }
    }

    public void OnDefocus() { }

    public void Interact(PlayerController player)
    {

    }

    private void PlaceItem(PlayerController player, IngredientData data)
    {
        player.Hand.ClearHand();

        GameObject obj = Instantiate(ingredientPrefab, itemSocket.position, Quaternion.identity);
        obj.transform.SetParent(itemSocket);
        obj.transform.localScale = Vector3.one;

        _placedIngredient = obj.GetComponent<Ingredient>();
        _placedIngredient.Initialize(data, false);

        processor.StartProcessing(data);
    }

    private void PickupItem(PlayerController player)
    {
        Debug.Log("Item Picked Up ");
        player.Hand.SetHeldItem(processor.GetProcessedData(), true);

        if (_placedIngredient != null)
        {
            Destroy(_placedIngredient.gameObject);
            _placedIngredient = null;
        }

        processor.Reset();
        stationView.ResetView();
    }
    private void HandleLogicCompletion()
    {
        if (_placedIngredient != null)
        {
            _placedIngredient.SetProcessed();
        }
    }
}