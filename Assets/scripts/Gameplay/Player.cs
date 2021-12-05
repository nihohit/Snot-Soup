using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour {
  [SerializeField] Transform _ingredientParent;

  private IngredientPicker _target;
  private IngredientPicker _pickedItem;
  private Cauldron _cauldron;

  private GameObject _smallPromptBox;
  private TMP_Text _descriptionText;

  private void Start() {
    _smallPromptBox = GameObject.Find("SmallPromptBox");
    _descriptionText = _smallPromptBox.transform.Find("Text").GetComponent<TMP_Text>();
    setIngredientDescription();
  }

  private void setIngredientDescription() {
    if (_target == null && _pickedItem == null) {
      _smallPromptBox.SetActive(false);
      return;
    }
    _smallPromptBox.SetActive(true);
    _descriptionText.text = (_target == null ? _pickedItem : _target).
  }

  public void SetPickupTarget(IngredientPicker target) {
    if (target == _pickedItem && target != null) {
      return;
    }
    _target = target;
    setIngredientDescription();
  }

  public bool CompareTarget(IngredientPicker target) {
    return target == _target;
  }

  private bool canPick() {
    return _target != null && _pickedItem == null;
  }

  private bool canDrop() {
    return _pickedItem != null;
  }

  public void OnInteract() {
    if (canPick()) {
      _pickedItem = _target;
      _target = null;
      _pickedItem.BindToTarget(_ingredientParent);
    } else if (_pickedItem != null) {
      _pickedItem.Unbind();
      if (_cauldron != null) {
        _pickedItem.SetPosition(_cauldron.IngredientDrop);
        _pickedItem.enabled = false;
      }
      _pickedItem = null;
    }
    setIngredientDescription();
  }

  public void OnCook() {
    _cauldron.Cook();
  }

  public void SetCauldron(Cauldron drop) {
    _cauldron = drop;
  }

}