using UnityEngine;

public class PlayerHandController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ingredientIcon;
    
    private IngredientData _heldData;
    private bool _isProcessed;

    public bool IsHandFull() => _heldData != null;

    public void SetHeldItem(IngredientData data, bool isProcessed)
    {
        _heldData = data;
        _isProcessed = isProcessed;
        UpdateVisuals();
    }

    public IngredientData GetHeldItemData() => _heldData;
    public bool IsHeldItemProcessed() => _isProcessed;

    public void ClearHand()
    {
        _heldData = null;
        _isProcessed = false;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (_heldData == null)
        {
            ingredientIcon.sprite = null;
            ingredientIcon.enabled = false;
            return;
        }

        ingredientIcon.enabled = true;
        ingredientIcon.sprite = _isProcessed ? _heldData.processedSprite : _heldData.rawSprite;
    }
}