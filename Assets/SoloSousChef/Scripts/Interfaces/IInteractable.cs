using com.SoloSousChef.Player;

namespace com.SoloSousChef.Interfaces
{
    public interface IInteractable
    {
        void Interact(PlayerController player);
        void OnFocus(PlayerController player);
        void OnDefocus();
    }
}