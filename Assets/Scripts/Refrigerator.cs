using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : MonoBehaviour, IInteractable
{
    [Header("UI & Visuals")]
    [SerializeField] private GameObject handsFullPrompt;
    [SerializeField] private Transform previewSpawnPoint;

    [Header("Settings")]
    [SerializeField] private List<IngredientData> allIngredients;
    [SerializeField] private GameObject ingredientBasePrefab;

    private Ingredient _previewInstance;
    private int _selectedIndex = 0;
    private bool _isFocused;

    private void OnEnable()
    {
        ToggleHandsFullPrompt(false);
    }
    public void OnFocus()
    {
        _isFocused = true;
        UpdateFridgeState();
    }

    public void OnDefocus()
    {
        _isFocused = false;
        ClearPreview();
        ToggleHandsFullPrompt(false);
    }

    private void Update()
    {
        if (_isFocused)
        {
            UpdateFridgeState();
        }
    }

    private void UpdateFridgeState()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        bool isPlayerFull = player != null && player.GetHeldIngredient() != null;

        if (isPlayerFull)
        {
            ClearPreview();
            ToggleHandsFullPrompt(true);
        }
        else
        {
            ToggleHandsFullPrompt(false);
            TryShowPreview();
        }
    }

    public void CycleSelection()
    {
        if (allIngredients.Count == 0 || _previewInstance == null) return;

        _selectedIndex = (_selectedIndex + 1) % allIngredients.Count;
        _previewInstance.Initialize(allIngredients[_selectedIndex]);
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
        if (_previewInstance != null || allIngredients.Count == 0) return;

        GameObject go = Instantiate(ingredientBasePrefab, previewSpawnPoint);
        go.transform.localPosition = Vector3.zero;
        _previewInstance = go.GetComponent<Ingredient>();
        _previewInstance.Initialize(allIngredients[_selectedIndex]);
    }

    private void ClearPreview()
    {
        if (_previewInstance != null)
        {
            Destroy(_previewInstance.gameObject);
            _previewInstance = null;
        }
    }

    private void ToggleHandsFullPrompt(bool isVisible)
    {
        if (handsFullPrompt != null && handsFullPrompt.activeSelf != isVisible)
        {
            handsFullPrompt.SetActive(isVisible);
        }
    }
}