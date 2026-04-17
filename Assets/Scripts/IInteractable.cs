public interface IInteractable
{
    void Interact(PlayerController player);
    void OnFocus();  
    void OnDefocus();
}