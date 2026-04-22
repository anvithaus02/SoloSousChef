using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace com.SoloSousChef.Order
{
    public class OrderItem : MonoBehaviour
    {
        [SerializeField] private Image ingredientIcon;
        [SerializeField] private TextMeshProUGUI ingredientName;
        [SerializeField] private GameObject deliveryStatusIcon;

        public void IntializeOrderItem(IngredientData ingredient)
        {
            SetIcon(ingredient.processedSprite);
            SetDeliveryStatusIcon(false);
        }

        public void SetDeliveryStatusIcon(bool isDelivered)
        {
            deliveryStatusIcon.SetActive(isDelivered);
        }

        private void SetIcon(Sprite icon)
        {
            ingredientIcon.sprite = icon;
        }

    }
}