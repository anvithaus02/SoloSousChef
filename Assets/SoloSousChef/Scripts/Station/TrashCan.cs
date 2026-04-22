using UnityEngine.UI;
using UnityEngine;
using com.SoloSousChef.Interfaces;
using com.SoloSousChef.Player;
namespace com.SoloSousChef.Station
{
    public class TrashCan : MonoBehaviour, IInteractable
    {
        [SerializeField] private Image trashBinIcon;
        [SerializeField] private Sprite openIcon;
        [SerializeField] private Sprite closeIcon;
        public void OnFocus(PlayerController player)
        {
            SetBinState(true);
        }
        public void OnDefocus()
        {
            SetBinState(false);
        }

        public void Interact(PlayerController player)
        {
            if (player.Hand.IsHandFull())
            {
                player.Hand.ClearHand();
            }
        }

        private void SetBinState(bool isOpen)
        {
            trashBinIcon.sprite = isOpen ? openIcon : closeIcon;
        }
    }
}