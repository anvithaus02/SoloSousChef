using UnityEngine;
using TMPro;
using com.SoloSousChef.UI.Components;

[System.Serializable]
public class ButtonVisualData
{
    public ButtonType type;
    public Sprite buttonSprite;
    public Color fontColor;
}

[CreateAssetMenu(fileName = "ButtonConfiguration", menuName = "SoloSousChef/UI/Button Configuration")]
public class ButtonConfigurationSO : ScriptableObject
{
    public ButtonVisualData[] buttonStyles;

    public ButtonVisualData GetData(ButtonType type)
    {
        return System.Array.Find(buttonStyles, x => x.type == type);
    }
}