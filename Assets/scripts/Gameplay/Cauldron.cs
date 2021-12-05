using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cauldron : MonoBehaviour {
  [SerializeField] Transform _ingredientDrop;
  [SerializeField] SnotSoup.BossGameObject _boss;

  private GameObject _smallPromptBox;
  private TMP_Text _descriptionText;

  private Dictionary<SnotSoup.FeedingResponse, string> _responses = new Dictionary<SnotSoup.FeedingResponse, string>() { { SnotSoup.FeedingResponse.Ate, "Looks good!" },
                                                                                                                           { SnotSoup.FeedingResponse.Refused, "Get that out of my face!" } };

  public Vector3 IngredientDrop { get { return _ingredientDrop.position; } }

  private List<SnotSoup.Gameplay.Ingredients.IngredientModel> _ingredients = new List<SnotSoup.Gameplay.Ingredients.IngredientModel>();

  private void Start() {
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

  private IEnumerator RotateText(Transform toRotate) {
    toRotate.Rotate(Vector3.forward, -20);
    for (var i = 0; i < 4; ++i) {
      var initialTime = System.DateTime.Now;
      while ((System.DateTime.Now - initialTime).TotalSeconds < 0.5) {
        var direction = i % 2 == 0 ? 1 : -1;
        yield return null;
        toRotate.Rotate(Vector3.forward, Time.deltaTime * 80 * direction);
      }
    }
    toRotate.rotation = Quaternion.identity;
    toRotate.gameObject.SetActive(false);
  }

  public void Cook() {
    if (_ingredients.Count == 0) {
      return;
    }
    var soup = SnotSoup.ResultCalculator.getSoupResult(new SnotSoup.CookingInputs(_ingredients));
    var response = _boss.TryFeed(soup);
    _smallPromptBox.SetActive(true);
    _descriptionText.text = _responses[response];
    _ingredients.Clear();
    StartCoroutine(RotateText(_smallPromptBox.transform));
  }
}
