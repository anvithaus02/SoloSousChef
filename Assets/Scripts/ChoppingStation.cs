using UnityEngine;

public class ChoppingStation : MonoBehaviour, IInteractable
{
    private float _currentTimer = 0;
    private bool _isChopping = false;
    private Ingredient _itemOnBoard;

    public void OnFocus(PlayerController playerController) => Debug.Log("<color=cyan>Chopping Board:</color> Player in range.");
    public void OnDefocus() => _isChopping = false;

    public void Interact(PlayerController player)
    {
        // // PICK UP logic
        // if (_itemOnBoard != null && player.GetHeldIngredient() == null)
        // {
        //     player.SetHeldIngredient(_itemOnBoard);
        //     _itemOnBoard = null;
        //     _isChopping = false;
        //     Debug.Log("Picked up processed item.");
        //     return;
        // }

        // // PLACE logic
        // if (_itemOnBoard == null && player.GetHeldIngredient() != null)
        // {
        //     Ingredient held = player.GetHeldIngredient();

        //     // VALIDATION: Check StationType and current Processed state
        //     if (held.Data.processingStation == StationType.Table && !held.IsProcessed)
        //     {
        //         _itemOnBoard = player.ReleaseHeldIngredient();
                
        //         // Snap to board (Parent to the trigger object)
        //         _itemOnBoard.transform.SetParent(this.transform);
        //         _itemOnBoard.transform.localPosition = Vector3.zero;

        //         _currentTimer = 0;
        //         _isChopping = true;
        //         Debug.Log($"Chopping {held.Data.ingredientName}...");
        //     }
        //     else
        //     {
        //         Debug.Log("<color=red>Action Blocked:</color> This item cannot be chopped here.");
        //     }
        // }
    }

    private void Update()
    {
        if (_isChopping && _itemOnBoard != null)
        {
            _currentTimer += Time.deltaTime;

            if (_currentTimer >= _itemOnBoard.Data.processingTime)
            {
                FinishChopping();
            }
        }
    }

    private void FinishChopping()
    {
        _isChopping = false;
        
        // Use your new method to swap the sprite and set the bool
        _itemOnBoard.SetProcessed();
        
        Debug.Log("<color=magenta>Chopping Complete!</color>");
    }
}