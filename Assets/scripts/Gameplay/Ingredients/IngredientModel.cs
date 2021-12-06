using UnityEngine;
using System.Linq;

namespace SnotSoup.Gameplay.Ingredients {
  /// <summary>
  /// Most values of the ingredients are in scale of 0f - 1f, in comparison to other ingredients in the inventory
  /// Some values are on the scale between -1f to 1f
  /// </summary>
  [CreateAssetMenu(fileName = "Ingredient Model", menuName = "Snot Soup /Ingredients/IngredientModel")]
  public class IngredientModel : ScriptableObject, IIngredientModel {
    [SerializeField] private string ingredientName;
    [Range(0f, 1f)]
    [SerializeField] private float size;
    [Range(0f, 1f)]
    [SerializeField] private float calories;
    [Range(0f, 1f)]
    [SerializeField] private float minerals;
    [Range(0f, 1f)]
    [SerializeField] private float vitamins;
    [Range(0f, 1f)]
    [SerializeField] private float saltiness;
    [Range(0f, 1f)]
    [SerializeField] private float bitterness;
    [Tooltip("When 3 = acidic, 5 = neutral, 10 = alkaline")]
    [Range(-1f, 1f)]
    [SerializeField] private float acidicPhLevel;
    [Range(-1f, 1f)]
    [SerializeField] private float sourSweetLevel;
    [Tooltip("How fast this product can get spoiled")]
    [Range(0f, 1f)]
    [SerializeField] private float freedomForDefects;
    [Range(0f, 1f)]
    [SerializeField] private float viscosity;

    #region Properties
    public string Name {
      get { return ingredientName; }
    }

    public float Size {
      get {
        return size;
      }
    }

    public float Calories {
      get {
        return calories;
      }
    }

    public float Minerals {
      get {
        return minerals;
      }
    }

    public float Vitamins {
      get {
        return vitamins;
      }
    }

    public float Saltiness {
      get {
        return saltiness;
      }
    }

    public float Bitterness {
      get {
        return bitterness;
      }
    }

    public float AcidicPhLevel {
      get {
        return acidicPhLevel;
      }
    }

    public float SourSweetLevel {
      get {
        return sourSweetLevel;
      }
    }

    public float FreedomForDefects {
      get {
        return freedomForDefects;
      }
    }

    public float Viscosity {
      get {
        return viscosity;
      }
    }

    public float SoupImpactScore {
      get { return size; }
    }

    public string Description {
      get {
        var splitName = string.Join("", Name.Select(letter => char.IsUpper(letter) ? " " + letter : "" + letter));
        if (splitName[0] == ' ') {
          splitName.Remove(0, 1);
        }
        var sizeDescription = size > 0.5 ? "large" : "small";
        var hardness = minerals + vitamins < size || viscosity > 0.3 ? "hard" : "soft";
        return $"{sizeDescription} {hardness} {splitName}".Replace("  ", " ");
      }
    }

    #endregion
  }
}