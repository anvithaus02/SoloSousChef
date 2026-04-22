using UnityEngine;
using UnityEngine.UI;
namespace com.SoloSousChef.Ingredients
{
    public class Ingredient : MonoBehaviour
    {
        [SerializeField] private Image _ingredientSprite;

        public IngredientData Data => _data;
        public bool IsProcessed => _isProcessed;
        private IngredientData _data;
        private bool _isProcessed;

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
            _ingredientSprite.sprite = _isProcessed ? _data.processedSprite : _data.rawSprite;
        }
    }
}