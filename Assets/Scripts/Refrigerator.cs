using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    [SerializeField] private List<IngredientData> allIngredients;
    [SerializeField] private GameObject ingredientBasePrefab;
    [SerializeField] private Transform previewSpawnPoint;

    private Ingredient _previewInstance;
    private int _selectedIndex = 0;

    public void OnFocus()
    {
        // We still call this, but the method will now check the player
        TryShowPreview();
    }

    public void OnDefocus()
    {
        ClearPreview();
    }

    public void CycleSelection()
    {
        if (allIngredients.Count == 0) return;

        _selectedIndex = (_selectedIndex + 1) % allIngredients.Count;

        if (_previewInstance != null)
        {
            _previewInstance.Initialize(allIngredients[_selectedIndex]);
        }
    }

    public void Interact(PlayerController player)
    {
        if (_previewInstance != null && player.GetHeldIngredient() == null)
        {
            player.SetHeldIngredient(_previewInstance);
            _previewInstance = null;
        }
    }

    private void TryShowPreview()
    {
        // Find the player in the scene to check their hands
        // (Or pass the player reference through the Sensor if preferred)
        PlayerController player = FindObjectOfType<PlayerController>();

        if (player != null && player.GetHeldIngredient() != null)
        {
            // Player's hands are full, don't show anything
            ClearPreview();
            return;
        }

        if (_previewInstance == null && allIngredients.Count > 0)
        {
            GameObject go = Instantiate(ingredientBasePrefab, previewSpawnPoint);
            go.transform.localPosition = Vector3.zero;

            _previewInstance = go.GetComponent<Ingredient>();
            _previewInstance.Initialize(allIngredients[_selectedIndex]);
        }
    }

    private void ClearPreview()
    {
        if (_previewInstance != null)
        {
            Destroy(_previewInstance.gameObject);
            _previewInstance = null;
        }
    }
}