using UnityEngine;
using System.Collections.Generic;

public class FridgeInteractionHandler : MonoBehaviour, IInteractable
{
    [Header("Dependencies")]
    [SerializeField] private FridgeCycleController cycleController;
    [SerializeField] private FridgeView fridgeView;
    [SerializeField] private GameObject ingredientPrefab;

    [Header("Settings")]
    [SerializeField] private List<IngredientData> availableIngredients;
    [SerializeField] private float cycleDuration = 1.0f;

    private void Awake()
    {
        cycleController.Initialize(availableIngredients, cycleDuration);
        
        cycleController.OnIngredientChanged += fridgeView.UpdateIngredientIcon;
        cycleController.OnCyclingStateChanged += fridgeView.SetDisplayActive;
    }

    public void OnFocus(PlayerController player)
    {
        if (player.GetHeldIngredient() != null)
        {
            fridgeView.ShowStatusMessage("HAND FULL!");
        }
        else
        {
            cycleController.StartCycle();
        }
    }

    public void OnDefocus()
    {
        cycleController.StopCycle();
    }

    public void Interact(PlayerController player)
    {
        if (player.GetHeldIngredient() != null)
        {
            fridgeView.ShowStatusMessage("HAND FULL!");
            return;
        }

        SpawnAndGiveIngredient(player);
    }

    private void SpawnAndGiveIngredient(PlayerController player)
    {
        IngredientData currentData = cycleController.GetCurrentIngredient();
        
        GameObject obj = Instantiate(ingredientPrefab);
        Ingredient ing = obj.GetComponent<Ingredient>();
        ing.Initialize(currentData);

        player.SetHeldIngredient(ing);

        cycleController.StopCycle();
    }
}