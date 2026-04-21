using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class FridgeView : MonoBehaviour
{
    [SerializeField] private IngredientBubble ingredientDisplayIcon;
    [SerializeField] private TextMeshProUGUI statusMessageText;
    [SerializeField] private float fadeDuration = 0.5f;

    [Header("State")]
    [SerializeField] private Image fridgeIcon;
    [SerializeField] private Sprite openIcon;
    [SerializeField] private Sprite closeIcon;

    private void Start()
    {
        SetDisplayActive(false);
    }

    public void SetDisplayActive(bool isActive)
    {
        ingredientDisplayIcon.SetDisplayState(isActive);
        SetFridgeState(isActive);
    }

    public void UpdateIngredientIcon(IngredientData data)
    {
        ingredientDisplayIcon.Initailize(data.rawSprite);
    }

    public void ShowStatusMessage(string message)
    {
        StopAllCoroutines();
        StartCoroutine(MessageFadeRoutine(message));
    }

    private System.Collections.IEnumerator MessageFadeRoutine(string msg)
    {
        statusMessageText.text = msg;
        statusMessageText.alpha = 1;
        yield return new WaitForSeconds(1.0f);
        float t = 1;
        while (t > 0)
        {
            t -= Time.deltaTime / fadeDuration;
            statusMessageText.alpha = t;
            yield return null;
        }
    }

    private void SetFridgeState(bool isOpen)
    {
        fridgeIcon.sprite = isOpen ? openIcon : closeIcon;
    }
}