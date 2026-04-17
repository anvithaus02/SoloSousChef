using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : MonoBehaviour, IInteractable
{
    [Header("Visuals")]
    [SerializeField] private SpriteRenderer _itemDisplayPreview;

    [Header("Data")]
    [SerializeField] private List<IngredientData> allIngredients;
    [SerializeField] private GameObject ingredientBasePrefab;

    private int _selectedIndex = 0;

    private void Start()
    {
        ToggleDisplayVisibility(false);
    }

    public void OnFocus()
    {
        UpdatePreviewVisuals();
        ToggleDisplayVisibility(true);
    }

    public void OnDefocus()
    {
        ToggleDisplayVisibility(false);
    }

    public void CycleSelection()
    {
        if (allIngredients.Count == 0) return;

        _selectedIndex = (_selectedIndex + 1) % allIngredients.Count;
        UpdatePreviewVisuals();
        
        Debug.Log($"[FRIDGE] Selected: {allIngredients[_selectedIndex].ingredientName}");
    }

    public void Interact(PlayerController player)
    {
        if (allIngredients.Count == 0) return;

        if (player.GetHeldIngredient() == null)
        {
            SpawnAndGiveItem(player);
        }
    }

    private void SpawnAndGiveItem(PlayerController player)
    {
        GameObject instance = Instantiate(ingredientBasePrefab);
        Ingredient ingredient = instance.GetComponent<Ingredient>();
        
        ingredient.Initialize(allIngredients[_selectedIndex]);
        
        player.SetHeldIngredient(ingredient);
    }

    private void UpdatePreviewVisuals()
    {
        if (allIngredients.Count > 0 && _itemDisplayPreview != null)
        {
            _itemDisplayPreview.sprite = allIngredients[_selectedIndex].rawSprite;
        }
    }

    private void ToggleDisplayVisibility(bool isVisible)
    {
        if (_itemDisplayPreview != null)
        {
            _itemDisplayPreview.gameObject.SetActive(isVisible);
        }
    }
}