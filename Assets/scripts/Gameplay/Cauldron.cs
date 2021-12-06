using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Cauldron : MonoBehaviour {
  [SerializeField] Transform _ingredientDrop;
  [SerializeField] SnotSoup.BossGameObject _boss;

  private GameObject _smallPromptBox;
  private TMP_Text _descriptionText;

  private System.Random _random = new System.Random();

  private Dictionary<SnotSoup.FeedingResponse, string[]> _responses = new Dictionary<SnotSoup.FeedingResponse, string[]>() {
      { SnotSoup.FeedingResponse.AteWell, new[] {
          "NOM NOM NOM",
          "THIS IS YUMMY",
          "I LIKE",
          "GIVE MOARRRRRR",
          "I WANT MOAR",
          "FOOD! FOODFOODFOOD",
        }},
      { SnotSoup.FeedingResponse.AteFeelingBad, new[] {
          "TUMMY FEEL BAD",
          "OOOOH NOOOOO",
          "TASTE WEIRD",
          "OHHHHHH, NOOOOES",
          "I SICK?",
          "MAYBE I HURL A BIT"
        }},
      { SnotSoup.FeedingResponse.Refused, new[] {
          "NO YUMMY",
          "BLEARGH",
          "I NO EAT DIS",
          "DIS LIKE POO",
          "DON'T WANNA"
      }}
};

  public Vector3 IngredientDrop { get { return _ingredientDrop.position; } }

  private List<SnotSoup.Gameplay.Ingredients.IngredientModel> _ingredients = new List<SnotSoup.Gameplay.Ingredients.IngredientModel>();

  private void Awake() {
    _smallPromptBox = GameObject.Find("SmallPromptBox");
    _descriptionText = _smallPromptBox.transform.Find("Text").GetComponent<TMP_Text>();
  }

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.CompareTag("Player")) {
      var p = other.GetComponent<Player>();
      p.SetCauldron(this);
    }
  }

  private void OnTriggerExit(Collider other) {
    if (other.gameObject.CompareTag("Player")) {
      var p = other.GetComponent<Player>();
      p.SetCauldron(null);
    }
  }

  public void Add(SnotSoup.Gameplay.Ingredients.IngredientModel ingredient) {
    _ingredients.Add(ingredient);
  }

  private IEnumerator RotateText(string textToDisplay) {
    _smallPromptBox.SetActive(true);
    _descriptionText.text = textToDisplay;
    return Rotate(_smallPromptBox.transform, 3, 2, Vector3.forward, 80, () => {
      _smallPromptBox.SetActive(false);
    });
  }

  private IEnumerator Rotate(Transform toRotate, int repetitions, float duration, Vector3 rotationAxis, float maxRotation,
  Action completion) {
    var twoDirectionRepetitions = repetitions * 2;
    var repetitionDuration = duration / twoDirectionRepetitions;
    toRotate.Rotate(rotationAxis, -maxRotation / twoDirectionRepetitions);
    for (var i = 0; i < twoDirectionRepetitions; ++i) {
      var initialTime = System.DateTime.Now;
      while ((System.DateTime.Now - initialTime).TotalSeconds < repetitionDuration) {
        var direction = i % 2 == 0 ? 1 : -1;
        yield return null;
        toRotate.Rotate(rotationAxis, Time.deltaTime * maxRotation * direction);
      }
    }
    toRotate.rotation = Quaternion.identity;
    completion();
  }

  public void Cook() {
    if (_ingredients.Count == 0) {
      StartCoroutine(RotateText("DIS IS WATER!\nMAKE SOUP!"));
      return;
    }
    var soup = SnotSoup.ResultCalculator.getSoupResult(new SnotSoup.CookingInputs(_ingredients));
    var response = _boss.TryFeed(soup);
    var responseList = _responses[response];
    StartCoroutine(RotateText(responseList[_random.Next(responseList.Length)]));
    _ingredients.Clear();
  }
}
