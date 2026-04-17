using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FridgeView : MonoBehaviour
{
    [SerializeField] private Image ingredientDisplayIcon;
    [SerializeField] private TextMeshProUGUI statusMessageText;
    [SerializeField] private float fadeDuration = 0.5f;

    public void SetDisplayActive(bool isActive)
    {
        ingredientDisplayIcon.gameObject.SetActive(isActive);
    }

    public void UpdateIngredientIcon(IngredientData data)
    {
        ingredientDisplayIcon.sprite = data.rawSprite;
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
}