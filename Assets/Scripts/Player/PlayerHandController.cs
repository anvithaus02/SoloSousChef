using UnityEngine;

public class PlayerHandController : MonoBehaviour
{
    [SerializeField] private Transform attachmentPoint;
    
    private Ingredient _heldIngredient;

    public bool IsHandFull() => _heldIngredient != null;

    public void SetHeldItem(Ingredient ingredient)
    {
        _heldIngredient = ingredient;
        
        // Ensure the sprite is visible above the player/background
        SpriteRenderer sr = _heldIngredient.transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (sr != null) sr.sortingOrder = 10;

        // Physical parenting
        _heldIngredient.transform.SetParent(attachmentPoint);
        _heldIngredient.transform.localPosition = Vector3.zero;
        _heldIngredient.transform.localRotation = Quaternion.identity;
    }

    public Ingredient GetHeldItem() => _heldIngredient;

    public Ingredient ReleaseItem()
    {
        Ingredient releasedItem = _heldIngredient;
        _heldIngredient = null;

        if (releasedItem != null)
        {
            releasedItem.transform.SetParent(null);
        }

        return releasedItem;
    }

    public void DestroyHeldItem()
    {
        if (_heldIngredient != null)
        {
            Destroy(_heldIngredient.gameObject);
            _heldIngredient = null;
        }
    }
}