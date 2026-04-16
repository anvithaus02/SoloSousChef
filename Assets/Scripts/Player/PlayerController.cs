using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSmoothness = 10f;

    [Header("Interactions")]
    [SerializeField] private Transform ingredientAttachmentPoint;
    
    private GameObject _heldIngredient;
    private InteractableStation _activeStation;
    private Camera _mainCamera;

    void OnEnable()
    {
        InteractableStation.OnStationEnter += SetActiveStation;
        InteractableStation.OnStationExit += ClearActiveStation;
    }

    void OnDisable()
    {
        InteractableStation.OnStationEnter -= SetActiveStation;
        InteractableStation.OnStationExit -= ClearActiveStation;
    }

    void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        ProcessMovement();
        ProcessInteractionInput();
    }

    private void SetActiveStation(InteractableStation station)
    {
        _activeStation = station;
        Debug.Log("Active Station being set : "+_activeStation);
    }

    private void ClearActiveStation(InteractableStation station)
    {
        if (_activeStation == station)
        {
            _activeStation = null;
            Debug.Log("Active Station being removed ");
        }
    }

    private void ProcessInteractionInput()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            if (_activeStation != null)
            {
                ExecuteStationLogic();
            }
        }
    }

    private void ExecuteStationLogic()
    {
        if (_activeStation.type == StationType.Trash && _heldIngredient != null)
        {
            Destroy(_heldIngredient);
            _heldIngredient = null;
        }
    }

    private void ProcessMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 rawDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (rawDirection.magnitude >= 0.1f)
        {
            Vector3 cameraForward = _mainCamera.transform.forward;
            Vector3 cameraRight = _mainCamera.transform.right;

            cameraForward.y = 0;
            cameraRight.y = 0;

            Vector3 worldMoveDirection = (cameraForward.normalized * rawDirection.z) + (cameraRight.normalized * rawDirection.x);

            transform.position += worldMoveDirection * movementSpeed * Time.deltaTime;

            Quaternion lookRotation = Quaternion.LookRotation(worldMoveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSmoothness * Time.deltaTime);
        }
    }
}