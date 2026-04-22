using com.SoloSousChef.UI.Components;
using UnityEngine;
namespace com.SoloSousChef.Player
{
    public class PlayerHandController : MonoBehaviour
    {
        [SerializeField] private IngredientBubble ingredientBubble;

        private IngredientData _heldData;
        private bool _isProcessed;

        public bool IsHandFull() => _heldData != null;

        private void Start()
        {
            UpdateVisuals();
        }

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
            Sprite sprite = null;
            if (_heldData != null)
                sprite = _isProcessed ? _heldData.processedSprite : _heldData.rawSprite;
            ingredientBubble.Initailize(sprite, _heldData == null ? 0.0f : 0.15f);
        }
    }
}