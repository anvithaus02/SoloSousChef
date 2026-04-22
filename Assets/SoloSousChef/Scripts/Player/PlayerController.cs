using com.SoloSousChef.Interfaces;
using UnityEngine;
namespace com.SoloSousChef.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject playerObject;
        [Header("Sub-Modules")]
        [SerializeField] private PlayerInputHandler inputHandler;
        [SerializeField] private PlayerLocomotion locomotion;
        [SerializeField] private PlayerInteractionHandler interactionHandler;
        [SerializeField] private PlayerHandController handController;
        [SerializeField] private PlayerInteractionSensor interactionSensor;

        public static PlayerController Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            SetPlayerDisplayState(false);
        }
        private void OnEnable()
        {
            interactionSensor.OnInteractableDetected += HandleDetection;
            interactionSensor.OnInteractableLost += HandleLost;

            inputHandler.OnInteractPressed += HandleInteractionRequest;

        }

        private void OnDisable()
        {
            interactionSensor.OnInteractableDetected -= HandleDetection;
            interactionSensor.OnInteractableLost -= HandleLost;
            inputHandler.OnInteractPressed -= HandleInteractionRequest;

        }

        private void Update()
        {
            locomotion.Move(inputHandler.MovementInput);
        }

        public void SetPlayerDisplayState(bool isVisible)
        {
            Debug.Log("Set Olayer " + isVisible);
            playerObject.SetActive(isVisible);
        }

        private void HandleDetection(IInteractable interactable)
        {
            // Delegate the logic to the Interaction Handler
            interactionHandler.HandleInteractableDetected(interactable, this);
        }

        private void HandleLost(IInteractable interactable)
        {
            interactionHandler.HandleInteractableLost(interactable);
        }

        private void HandleInteractionRequest()
        {
            interactionHandler.PerformInteraction(this);
        }

        public PlayerHandController Hand => handController;
    }
}