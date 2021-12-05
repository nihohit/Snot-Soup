using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    [SerializeField] Transform _ingredientDrop;

    private List<SnotSoup.Gameplay.Ingredients.IngredientModel> ingredients = new List<SnotSoup.Gameplay.Ingredients.IngredientModel>();

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

    public void Add(SnotSoup.Gameplay.Ingredients.IngredientModel ingredient)
    {
        ingredients.Add(ingredient);
    }
}
