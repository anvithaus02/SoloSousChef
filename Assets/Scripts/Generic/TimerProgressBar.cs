using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class TimerProgressBar : MonoBehaviour
{
    public enum DisplayMode { Increasing, Decreasing }

    [Header("UI References")]
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI label;
    
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void UpdateTimer(float current, float max, DisplayMode mode)
    {
        float ratio = Mathf.Clamp01(current / max);
        
        fillImage.fillAmount = (mode == DisplayMode.Increasing) ? ratio : (1f - ratio);

        if (label != null)
        {
            label.text = $"{current:F1}sec";
        }
    }

    public void SetVisibility(bool isVisible)
    {
        if (_canvasGroup == null) _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = isVisible ? 1f : 0f;
    }
}