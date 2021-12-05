using System.Collections.Generic;
using SnotSoup.Gameplay.Ingredients;

namespace SnotSoup {

  public class FinishedSoup {
    public float toxicity;
    public float yumminess;
    public float looks;
  }

  public class CookingInputs {
      public List<IngredientModel> Ingredients = new List<IngredientModel>();
  }

  public class ResultCalculator {
    private static float computeToxicity(CookingInputs inputs) {
      var ingredients = inputs.Ingredients;
      var computedToxicity = 0f;
      var acidity = 0f;
      var freedomOfDefect = 0f;
      var size = 0f;
      var calories = 0f;
      
      for (int i = 0; i < ingredients.Count; i++) {
        acidity += ingredients[i].AcidicPhLevel;
        freedomOfDefect += ingredients[i].FreedomForDefects;
        size += ingredients[i].Size;
        calories += ingredients[i].Calories;
      }

      computedToxicity = acidity * freedomOfDefect * size * calories;
      return computedToxicity;
    }

    private static float computeYumminess(CookingInputs inputs) {
      var ingredients = inputs.Ingredients;
      var computedYumminess = 0f;
      var size = 0f;
      var vitamins = 0f;
      var minerals = 0f;
      var acidicValue = 0f;
      var bitterness = 0f;
      var sourSweetLevel = 0f;
      var saltiness = 0f;

      for (int i = 0; i < ingredients.Count; i++) {
        size += ingredients[i].Size;
        vitamins += ingredients[i].Vitamins;
        minerals += ingredients[i].Minerals;
        acidicValue += ingredients[i].AcidicPhLevel;
        bitterness += ingredients[i].Bitterness;
        sourSweetLevel += ingredients[i].SourSweetLevel;
        saltiness += ingredients[i].Saltiness;
      }

      // Multiply by -1 for values which give negative impact, and multiply by 0.5 for values which give best effort at its center
      computedYumminess = size * vitamins * minerals * (0.5f * acidicValue) * (-1 * bitterness) * sourSweetLevel * (0.5f * saltiness);

      return computedYumminess;
    }

    private static float computeLooks(CookingInputs inputs) {
      var ingredients = inputs.Ingredients;
      var computedLooks = 0f;
      var freedomForDefect = 0f;
      var viscocity = 0f;

      for (int i = 0; i < ingredients.Count; i++) {
        freedomForDefect += ingredients[i].FreedomForDefects;
        viscocity += ingredients[i].Viscosity;
      }

      computedLooks = freedomForDefect * viscocity;
      
      return computedLooks;
    }

    public static FinishedSoup getSoupResult(CookingInputs inputs) {
      return new FinishedSoup {
        toxicity = computeToxicity(inputs),
        yumminess = computeYumminess(inputs),
        looks = computeLooks(inputs)
      };
    }
  }
}