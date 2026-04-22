using UnityEngine;
using System;
namespace com.SoloSousChef.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public event Action OnInteractPressed;
        public event Action OnSelectionPressed;

        public Vector2 MovementInput { get; private set; }

        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            MovementInput = new Vector2(horizontal, vertical).normalized;

            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
            {
                OnInteractPressed?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                OnSelectionPressed?.Invoke();
            }
        }
    }
}