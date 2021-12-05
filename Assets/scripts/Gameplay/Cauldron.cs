using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cauldron : MonoBehaviour
{
    [SerializeField] Transform _ingredientDrop;
    [SerializeField] SnotSoup.BossGameObject _boss; 
    
    private GameObject _smallPromptBox;
    private TMP_Text _descriptionText;

    private Dictionary<SnotSoup.FeedingResponse, string> _responses = new Dictionary<SnotSoup.FeedingResponse, string>() { { SnotSoup.FeedingResponse.Ate, "Looks good!" },
                                                                                                                           { SnotSoup.FeedingResponse.Refused, "Get that out of my face!" } };

    public Vector3 IngredientDrop { get { return _ingredientDrop.position; } }

    private List<SnotSoup.Gameplay.Ingredients.IngredientModel> _ingredients = new List<SnotSoup.Gameplay.Ingredients.IngredientModel>();

    private void Start()
    {
        _smallPromptBox = GameObject.Find("SmallPromptBox");
        _descriptionText = _smallPromptBox.transform.Find("Text").GetComponent<TMP_Text>();
    }

        private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var p = other.GetComponent<Player>();
            p.SetCauldron(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var p = other.GetComponent<Player>();
            p.SetCauldron(null);
        }
    }

    public void Add(SnotSoup.Gameplay.Ingredients.IngredientModel ingredient)
    {
        _ingredients.Add(ingredient);
    }

    public void Cook()
    {
        var soup = SnotSoup.ResultCalculator.getSoupResult(new SnotSoup.CookingInputs(_ingredients));
        var response = _boss.TryFeed(soup);
        _smallPromptBox.SetActive(true);
        _descriptionText.text = _responses[response];
        _ingredients.Clear();
    }
}
