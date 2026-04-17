using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "FridgeConfig", menuName = "Kitchen/Fridge Configuration")]
public class FridgeDataConfig : ScriptableObject
{
    public List<IngredientData> availableIngredients;
    public float cycleDuration = 1.0f;
}