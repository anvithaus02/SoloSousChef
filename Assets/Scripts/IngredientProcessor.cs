using UnityEngine;
using System;

public class IngredientProcessor : MonoBehaviour
{
    public event Action<float, float> OnProgressUpdated;
    public event Action OnProcessingComplete;

    private IngredientData _currentData;
    private float _timer;
    private bool _isProcessing;

    public bool IsComplete { get; private set; }
    public bool IsBusy => _isProcessing;

    private void Update()
    {
        if (!_isProcessing || IsComplete) return;

        _timer += Time.deltaTime;
        OnProgressUpdated?.Invoke(_timer, _currentData.processingTime);

        if (_timer >= _currentData.processingTime)
        {
            CompleteProcessing();
        }
    }

    public void StartProcessing(IngredientData data)
    {
        _currentData = data;
        _timer = 0;
        _isProcessing = true;
        IsComplete = false;
    }

    public IngredientData GetProcessedData()
    {
        return _currentData;
    }

    private void CompleteProcessing()
    {
        _isProcessing = false;
        IsComplete = true;
        OnProcessingComplete?.Invoke();
    }

    public void Reset()
    {
        _isProcessing = false;
        IsComplete = false;
        _timer = 0;
    }
}