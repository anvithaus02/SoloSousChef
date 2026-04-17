public interface IInteractable
{
    void Interact(PlayerController player);
    void OnFocus(PlayerController player);  
    void OnDefocus();
}