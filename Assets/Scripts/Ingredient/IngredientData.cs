using UnityEngine;

[CreateAssetMenu(fileName = "NewIngredient", menuName = "Kitchen/Ingredient Data")]
public class IngredientData : ScriptableObject
{
    public string ingredientName;
    public Sprite rawSprite;
    public Sprite processedSprite;
    public int scoreValue; 
    public StationType processingStation; 
    public float processingTime; 
}