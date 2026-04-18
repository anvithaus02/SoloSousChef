using UnityEngine;

public class GamePlayScreen : BaseScreen
{
    [SerializeField] private GamePlayHUD gamePlayHUD;

    // Overriding Show ensures that every time the gameplay starts, 
    // the HUD and underlying systems are refreshed.
    public override void Show(bool animate = true)
    {
        base.Show(animate);
        Initialize();
    }

    private void Initialize()
    {
        // Reset the score for the new 3-minute session
        ScoreManager.Instance.ResetScore();
        
        // Initialize HUD components (Buttons, initial text states)
        gamePlayHUD.Initialize();
        
        Debug.Log("Gameplay Screen Initialized: Score Reset and HUD Ready.");
    }
}