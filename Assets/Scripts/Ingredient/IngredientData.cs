using UnityEngine;

[CreateAssetMenu(fileName = "NewIngredient", menuName = "Kitchen/Ingredient")]
public class IngredientData : ScriptableObject
{
    public string ingredientName;
    [Header("Raw State")]
    public Sprite rawSprite;
    public StationType processingStation;
    public float processingTime = 3f;
    public int scoreValue;

    [Header("Processed State")]
    public Sprite processedSprite;

    [HideInInspector] public bool RequiresProcessing => processingStation != StationType.None;
}