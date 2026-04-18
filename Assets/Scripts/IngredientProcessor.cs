using UnityEngine;
using System;

public class IngredientProcessor : MonoBehaviour
{
    public event Action<float> OnProcessingStarted; 
    public event Action OnProcessingComplete;

    private IngredientData _currentData;
    private TickTimer _processorTimer;
    
    public bool IsComplete { get; private set; }
    public bool IsBusy => _processorTimer != null && _processorTimer.IsRunning;

    public void StartProcessing(IngredientData data)
    {
        _currentData = data;
        IsComplete = false;

        OnProcessingStarted?.Invoke(data.processingTime);

        _processorTimer = new TickTimer(this, (int)data.processingTime, true, 
            null, 
            CompleteProcessing
        );
    }

    public IngredientData GetProcessedData()
    {
        return _currentData;
    }

    private void CompleteProcessing()
    {
        IsComplete = true;
        OnProcessingComplete?.Invoke();
    }

    public void Reset()
    {
        if (_processorTimer != null) _processorTimer.Stop(false);
        IsComplete = false;
    }
}