using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private Image _ingredientSprite;
    private IngredientData _data;
    private bool _isProcessed;

    public IngredientData Data => _data;
    public bool IsProcessed => _isProcessed;

    public void Initialize(IngredientData data, bool isProcessed = false)
    {
        _data = data;
        _isProcessed = isProcessed;

        UpdateVisuals();
    }

    public void SetProcessed()
    {
        _isProcessed = true;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (_data == null) return;

        _ingredientSprite.sprite = _isProcessed ? _data.processedSprite : _data.rawSprite;
    }
}