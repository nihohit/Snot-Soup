using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform _ingredientParent;

    private IngredientPicker _target;
    private bool _canPick = true;
    private IngredientPicker _pickedItem;
    private Cauldron _cauldron;


    public void SetPickupTarget(IngredientPicker target)
    {
        _target = target;
    }

    public bool CompareTarget(IngredientPicker target)
    {
        return target == _target;
    }

    public void OnInteract()
    {
        if (_target != null && _canPick)
        {
            _pickedItem = _target;
            _pickedItem.BindToTarget(_ingredientParent);
            _canPick = false;
        }
        else if (_target != null)
        {
            _pickedItem.Unbind();
            if (_cauldron != null)
            {
                _pickedItem.SetPosition(_cauldron.IngredientDrop);
            }
            _pickedItem = null;
            _canPick = true;
        }
    }

    public void SetCauldron(Cauldron drop)
    {
        _cauldron = drop;
    }
}