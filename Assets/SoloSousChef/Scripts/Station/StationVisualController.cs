using UnityEngine;

public class StationVisualController : MonoBehaviour
{
    [SerializeField] private IngredientProcessor processor;
    [SerializeField] private TimerProgressBar progressBar;

    private void OnEnable()
    {
        processor.OnProcessingStarted += HandleProcessingStarted;
        processor.OnProcessingComplete += HandleProcessingComplete;
    }

    private void OnDisable()
    {
        processor.OnProcessingStarted -= HandleProcessingStarted;
        processor.OnProcessingComplete -= HandleProcessingComplete;
    }

    private void HandleProcessingStarted(float duration)
    {
        progressBar.StartProgress(duration, TimerProgressBar.DisplayMode.Decreasing);
    }

    private void HandleProcessingComplete()
    {
        ResetView();
    }

    public void ResetView()
    {
        progressBar.StopProgress();
    }
}