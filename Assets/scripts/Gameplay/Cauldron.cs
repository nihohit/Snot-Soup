using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    [SerializeField] Transform _ingredientDrop;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var p = other.GetComponent<Player>();
            p.SetIngredientDrop(_ingredientDrop);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var p = other.GetComponent<Player>();
            p.SetIngredientDrop(null);
        }
    }
}
