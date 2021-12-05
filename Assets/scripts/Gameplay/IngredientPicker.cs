using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPicker : MonoBehaviour
{
    [SerializeField] Transform _ingredient;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var p = other.GetComponent<Player>();
            p.SetPickupTarget(_ingredient);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var p = other.GetComponent<Player>();
            p.SetPickupTarget(null);
        }
    }
}
