using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OrderItem : MonoBehaviour
{
    [SerializeField] private Image ingredientIcon;
    [SerializeField] private TextMeshProUGUI ingredientName;
    [SerializeField] private GameObject deliveryStatusIcon;

    public void IntializeOrderItem(IngredientData ingredient)
    {
        SetIcon(ingredient.processedSprite);
        SetName(ingredient.ingredientName);
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

    private void SetName(string name)
    {
        ingredientName.text = name;
    }
}
