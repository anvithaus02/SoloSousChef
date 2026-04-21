using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public enum ButtonType
{
    Primary,
    Secondary,
    Quit,
    Pause
}
public class ActionButton : MonoBehaviour
{
    [SerializeField] private Button actionButton;
    [SerializeField] private RectTransform pressAnimTransform;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Image buttonBG;
    [SerializeField] private ButtonConfigurationSO buttonConfig;


    private Action _onButtonClickCallback;

    private void OnEnable()
    {
        actionButton.onClick.AddListener(HandleButtonClick);
    }

    private void OnDisable()
    {
        actionButton.onClick.RemoveListener(HandleButtonClick);
        pressAnimTransform.DOKill();
    }

    public void Initialize(ButtonType buttonType, string text, bool isInteractable, Action onButtonClick)
    {
        SetButtonUI(buttonType);
        SetButtonTextState(text);
        actionButton.interactable = isInteractable;
        _onButtonClickCallback = onButtonClick;
    }

    public void SetInteractability(bool isInteractable)
    {
        actionButton.interactable = isInteractable;
    }

    private void SetButtonUI(ButtonType buttonType)
    {
        ButtonVisualData data = buttonConfig.GetData(buttonType);
        buttonBG.sprite = data.buttonSprite;
        buttonText.color = data.fontColor;
    }

    private void SetButtonTextState(string text)
    {
        bool isTextPresent = !string.IsNullOrEmpty(text);
        buttonText.gameObject.SetActive(isTextPresent);

        if (isTextPresent)
        {
            buttonText.text = text;
        }
    }

    private void HandleButtonClick()
    {
        PlayPressAnim();
        _onButtonClickCallback?.Invoke();
    }

    private void PlayPressAnim()
    {
        if (pressAnimTransform == null)
            return;

        pressAnimTransform.localScale = Vector3.one;
        pressAnimTransform.DOPunchScale(new Vector3(-0.1f, -0.1f, -0.1f), 0.1f, 0, 0)
                          .SetUpdate(true);
    }
}