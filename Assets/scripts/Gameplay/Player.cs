using System;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour {
  [SerializeField] Transform _ingredientParent;
  [SerializeField] private Animator animator;

  [SerializeField] private string pickupAnimationTriggerName = "Pickup";
  [SerializeField] private string placeAnimationTriggerName = "Place";
  [SerializeField] private string runAnimationBoolName = "RunWithIngredient";

  [SerializeField] ESCMenu _escMenu;

  private IngredientPicker _target;
  private IngredientPicker _pickedItem;
  private Cauldron _cauldron;

  private GameObject _smallPromptBox;
  private TMP_Text _descriptionText;
  private int _pickupAnimationHash;
  private int _placeAnimationHash;
  private int _runAnimationHash;

  protected void Awake() {
    _pickupAnimationHash = Animator.StringToHash(pickupAnimationTriggerName);
    _placeAnimationHash = Animator.StringToHash(placeAnimationTriggerName);
    _runAnimationHash = Animator.StringToHash(runAnimationBoolName);
  }

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
    var foodDescription = (_pickedItem != null ? _pickedItem : _target).GetDescription();
    var actionDescription = _pickedItem == null ? "Pick up" : _cauldron == null ? "Drop" : "Cook";
    _descriptionText.text = $"{actionDescription} {foodDescription}";
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
      animator.SetTrigger(_pickupAnimationHash);
      _pickedItem = _target;
      _target = null;
      _pickedItem.BindToTarget(_ingredientParent);
    } else if (_pickedItem != null) {
      _pickedItem.Unbind();
      animator.SetTrigger(_placeAnimationHash);
      if (_cauldron != null) {
        _pickedItem.SetPosition(_cauldron.IngredientDrop);
        _pickedItem.enabled = false;
      }
      _pickedItem = null;
    }
    setIngredientDescription();
  }

  public void OnCook() {
    if (_cauldron != null) {
      _cauldron.Cook();
    }
  }

  public void SetCauldron(Cauldron drop) {
    _cauldron = drop;
    setIngredientDescription();
  }

  public void OnESC() {
    _escMenu.ToggleActive(true);
    _escMenu.OnResume += Resume;
  }

  private void Resume() {
    _escMenu.OnResume -= Resume;
    _escMenu.ToggleActive(false);
  }
}