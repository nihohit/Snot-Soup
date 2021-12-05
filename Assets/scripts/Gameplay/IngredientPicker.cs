using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPicker : MonoBehaviour {
  [SerializeField] GameObject _ingredient;

  private Rigidbody _rb;
  private SnotSoup.Gameplay.Ingredients.IngredientView _ingredientView;

  public string Name { get { return _ingredientView.Name; } }

  protected void Start() {
    _rb = _ingredient.GetComponent<Rigidbody>();
    _ingredientView = _ingredient.GetComponent<SnotSoup.Gameplay.Ingredients.IngredientView>();
  }

  private void OnTriggerEnter(Collider other) {
    if (!enabled) {
      return;
    }
    if (other.gameObject.CompareTag("Player")) {
      var p = other.GetComponent<Player>();
      p.SetPickupTarget(this);
    }
  }

  private void OnTriggerExit(Collider other) {
    if (other.gameObject.CompareTag("Player")) {
      var p = other.GetComponent<Player>();
      if (p.CompareTarget(this))
        p.SetPickupTarget(null);
    }
  }

  public void BindToTarget(Transform target) {
    _rb.isKinematic = true;
    _ingredient.transform.parent = target;
    _ingredient.transform.localPosition = Vector3.zero;
  }

  public void Unbind() {
    _rb.isKinematic = false;
    _ingredient.transform.parent = null;
  }

  public void SetPosition(Vector3 pos) {
    _ingredient.transform.position = pos;
  }

  public string GetDescription() {
    return _ingredientView.Description;
  }
}
