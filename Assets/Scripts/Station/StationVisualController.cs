using UnityEngine;

public class StationVisualController : MonoBehaviour
{
    [SerializeField] private IngredientProcessor processor;
    [SerializeField] private TimerProgressBar progressBar;

    private void OnEnable()
    {
        processor.OnProgressUpdated += HandleProgressUpdate;
        processor.OnProcessingComplete += HandleProcessingComplete;
    }

    private void OnDisable()
    {
        processor.OnProgressUpdated -= HandleProgressUpdate;
        processor.OnProcessingComplete -= HandleProcessingComplete;
    }

    private void HandleProgressUpdate(float current, float max)
    {
        if (!progressBar.gameObject.activeSelf)
        {
            progressBar.SetVisibility(true);
        }

        progressBar.UpdateTimer(current, max, TimerProgressBar.DisplayMode.Decreasing);
    }

    private void HandleProcessingComplete()
    {
        progressBar.SetVisibility(false);
    }

    public void ResetView()
    {
        progressBar.SetVisibility(false);
        progressBar.UpdateTimer(0, 1, TimerProgressBar.DisplayMode.Decreasing);
    }
}