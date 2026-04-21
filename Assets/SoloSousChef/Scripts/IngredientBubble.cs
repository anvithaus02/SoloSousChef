using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class IngredientBubble : MonoBehaviour
{
    [SerializeField] private Transform ingredientBubble;
    [SerializeField] private Image ingredientIcon;

    public void Initailize(Sprite icon, float scaleValue = 1.0f)
    {
        ingredientBubble.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
        ingredientIcon.sprite = icon;
    }
}
