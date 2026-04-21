using UnityEngine;

public class UIAnchorFollower : MonoBehaviour
{
    [SerializeField] private Transform targetAnchor;
    [SerializeField] private RectTransform movePoint;
    [SerializeField] private Vector3 offset;

    private Camera _mainCam;

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void LateUpdate()
    {
        if (targetAnchor == null || movePoint == null) return;

        Vector3 screenPosition = _mainCam.WorldToScreenPoint(targetAnchor.position);

        if (screenPosition.z < 0)
        {
            movePoint.gameObject.SetActive(false);
            return;
        }
        movePoint.position = screenPosition + offset;
    }
}