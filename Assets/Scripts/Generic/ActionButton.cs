using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ActionButton : MonoBehaviour
{
    [SerializeField] private Button actionButton;
    [SerializeField] private RectTransform pressAnimTransform;
    [SerializeField] private TextMeshProUGUI buttonText;

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

    public void Initialize(string text, bool isInteractable, Action onButtonClick)
    {
        buttonText.text = text;
        actionButton.interactable = isInteractable;
        _onButtonClickCallback = onButtonClick;
    }

    public void SetInteractability(bool isInteractable)
    {
        actionButton.interactable = isInteractable;
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