using UnityEngine;

public class ChoppingStationView : MonoBehaviour
{
    [SerializeField] private TimerProgressBar progressBar;
    [SerializeField] private IngredientProcessor processor;

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

    private void Start()
    {
        ResetView();
    }

    private void HandleProgressUpdate(float current, float max)
    {
        progressBar.SetVisibility(true);
        progressBar.UpdateTimer(current, max, TimerProgressBar.DisplayMode.Increasing);
    }

    private void HandleProcessingComplete()
    {
        progressBar.SetVisibility(false);
    }

    public void ResetView()
    {
        progressBar.SetVisibility(false);
    }
}