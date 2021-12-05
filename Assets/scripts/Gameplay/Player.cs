using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform _ingredientParent;

    private IngredientPicker _target;
    private bool _canPick = true;
    private IngredientPicker _pickedItem;
    private Transform _ingredientDrop;


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
            if (_ingredientDrop != null)
            {
                _pickedItem.SetPosition(_ingredientDrop.position);
            }
            _pickedItem = null;
            _canPick = true;
        }
    }

    public void SetIngredientDrop(Transform drop)
    {
        _ingredientDrop = drop;
    }
}