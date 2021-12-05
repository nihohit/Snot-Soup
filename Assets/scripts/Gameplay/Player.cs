using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform _ingredientParent;

    private Transform _target;
    private bool _canPick = true;

    public void SetPickupTarget(Transform target)
    {
        _target = target;
    }

    public void OnInteract()
    {
        if (_target != null && _canPick)
        {
            _target.parent = _ingredientParent;
            _target.localPosition = Vector3.zero;
            _canPick = false;
        }
        else if (_target != null)
        {
            _target.parent = null;
            _canPick = true;
        }
    }
}
