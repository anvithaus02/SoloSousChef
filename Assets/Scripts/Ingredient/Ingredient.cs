using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _ingredientSprite;
    private IngredientData _data;
    private bool _isProcessed;

    public IngredientData Data => _data;
    public bool IsProcessed => _isProcessed;

    public void Initialize(IngredientData newData)
    {
        _data = newData;
        _isProcessed = false;
        UpdateVisuals(false);
    }

    public void SetProcessed()
    {
        _isProcessed = true;
        UpdateVisuals(true);
    }

    private void UpdateVisuals(bool isProcessed)
    {
        _ingredientSprite.sprite = isProcessed ? _data.processedSprite: _data.rawSprite;
    }
}