using System.Collections;
using UnityEngine;

public class TrashCan : MonoBehaviour, IInteractable
{
    [Header("Color Settings")]
    [SerializeField] private Renderer _meshRenderer;
    [SerializeField] private Color _defaultColor = Color.gray;
    [SerializeField] private Color _hoverColor = Color.yellow;
    [SerializeField] private Color _trashActionColor = Color.red;

    private void Start()
    {
        SetColor(_defaultColor);
    }

    public void OnFocus()
    {
        SetColor(_hoverColor);
    }

    public void OnDefocus()
    {
        SetColor(_defaultColor);
    }

    public void Interact(PlayerController player)
    {
        if (player.GetHeldIngredient() != null)
        {
            player.DestroyHeldIngredient();
            StartCoroutine(FlashTrashColor());
        }
    }

    private IEnumerator FlashTrashColor()
    {
        SetColor(_trashActionColor);
        yield return new WaitForSeconds(0.5f);
        SetColor(_hoverColor);
    }

    private void SetColor(Color color)
    {
        _meshRenderer.material.color = color;
    }
}