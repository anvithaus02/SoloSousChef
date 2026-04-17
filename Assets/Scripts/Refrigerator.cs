using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Refrigerator : MonoBehaviour, IInteractable
{
    [Header("Data")]
    [SerializeField] private List<IngredientData> availableIngredients;
    [SerializeField] private GameObject ingredientPrefab;

    [Header("UI References")]
    [SerializeField] private Image ingredientDisplayIcon;
    [SerializeField] private TextMeshProUGUI statusMessageText;

    [Header("Settings")]
    [SerializeField] private float cycleDuration = 1.0f;
    [SerializeField] private float messageDuration = 1.5f;

    private int _currentIndex = 0;
    private bool _isCycling = false;
    private Coroutine _cycleCoroutine;
    private Coroutine _messageCoroutine;

    private void Start()
    {
        if (ingredientDisplayIcon != null) ingredientDisplayIcon.gameObject.SetActive(false);
        if (statusMessageText != null) statusMessageText.alpha = 0;
    }

    public void OnFocus(PlayerController player)
    {
        if (player.Hand.GetHeldItem() != null)
        {
            ShowStatusMessage("Hand Full!");
        }
        else
        {
            StartCycle();
        }
    }

    public void OnDefocus()
    {
        ResetFridge();
        Debug.Log("<color=gray>Fridge:</color> Energy OFF. Resetting.");
    }

    public void Interact(PlayerController player)
    {
        // 1. Check if Hand is Full
        if (player.Hand.GetHeldItem() != null)
        {
            ShowStatusMessage("Hand Full!");
            return;
        }

        // 2. If Hand is Empty, take the currently visible item
        if (_isCycling)
        {
            GiveItemToPlayer(player);
        }
    }

    public void CycleSelection()
    {
        // Manual skip logic if they press Q
        if (_isCycling)
        {
            StopCoroutine(_cycleCoroutine);
            MoveToNextIngredient();
            _cycleCoroutine = StartCoroutine(CycleRoutine());
        }
    }

    private void StartCycle()
    {
        if (availableIngredients == null || availableIngredients.Count == 0) return;

        _isCycling = true;
        _currentIndex = 0; // Always start at 0
        ingredientDisplayIcon.gameObject.SetActive(true);
        UpdateIcon();

        if (_cycleCoroutine != null) StopCoroutine(_cycleCoroutine);
        _cycleCoroutine = StartCoroutine(CycleRoutine());
    }

    private void ResetFridge()
    {
        _isCycling = false;
        if (_cycleCoroutine != null) StopCoroutine(_cycleCoroutine);

        _currentIndex = 0;
        ingredientDisplayIcon.gameObject.SetActive(false);
    }

    private IEnumerator CycleRoutine()
    {
        while (_isCycling)
        {
            yield return new WaitForSeconds(cycleDuration);
            MoveToNextIngredient();
        }
    }

    private void MoveToNextIngredient()
    {
        _currentIndex = (_currentIndex + 1) % availableIngredients.Count;
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        ingredientDisplayIcon.sprite = availableIngredients[_currentIndex].rawSprite;
    }

    private void GiveItemToPlayer(PlayerController player)
    {
        GameObject obj = Instantiate(ingredientPrefab);
        Ingredient ing = obj.GetComponent<Ingredient>();
        ing.Initialize(availableIngredients[_currentIndex]);

        player.Hand.SetHeldItem(ing);

        // Hide UI and stop cycling until they walk away and come back
        StopCycleTemporarily();
    }

    private void StopCycleTemporarily()
    {
        _isCycling = false;
        if (_cycleCoroutine != null) StopCoroutine(_cycleCoroutine);
        ingredientDisplayIcon.gameObject.SetActive(false);
    }

    private void ShowStatusMessage(string message)
    {
        if (_messageCoroutine != null) StopCoroutine(_messageCoroutine);
        _messageCoroutine = StartCoroutine(MessageFadeRoutine(message));
    }

    private IEnumerator MessageFadeRoutine(string msg)
    {
        statusMessageText.text = msg;
        statusMessageText.alpha = 1;
        yield return new WaitForSeconds(messageDuration);

        float t = 1;
        while (t > 0)
        {
            t -= Time.deltaTime;
            statusMessageText.alpha = t;
            yield return null;
        }
    }
}