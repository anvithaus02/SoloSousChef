using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeCycleController : MonoBehaviour
{
    public event Action<IngredientData> OnIngredientChanged;
    public event Action<bool> OnCyclingStateChanged;

    private List<IngredientData> _ingredients;
    private float _cycleDuration;
    private int _currentIndex;
    private bool _isCycling;
    private Coroutine _cycleCoroutine;

    public void Initialize(List<IngredientData> data, float duration)
    {
        _ingredients = data;
        _cycleDuration = duration;
    }

    public void StartCycle()
    {
        if (_ingredients == null || _ingredients.Count == 0) return;

        _isCycling = true;
        _currentIndex = 0;

        OnCyclingStateChanged?.Invoke(true);
        OnIngredientChanged?.Invoke(_ingredients[_currentIndex]);

        if (_cycleCoroutine != null) StopCoroutine(_cycleCoroutine);
        _cycleCoroutine = StartCoroutine(CycleRoutine());
    }

    public void StopCycle()
    {
        _isCycling = false;
        if (_cycleCoroutine != null) StopCoroutine(_cycleCoroutine);
        OnCyclingStateChanged?.Invoke(false);
    }

    public IngredientData GetCurrentIngredient()
    {
        return _ingredients[_currentIndex];
    }

    private IEnumerator CycleRoutine()
    {
        while (_isCycling)
        {
            yield return new WaitForSeconds(_cycleDuration);
            _currentIndex = (_currentIndex + 1) % _ingredients.Count;
            OnIngredientChanged?.Invoke(_ingredients[_currentIndex]);
        }
    }
}