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
      
      //1. first calculate the values into one value "pool"
      for (int i = 0; i < ingredients.Count; i++) {
        acidity += ingredients[i].AcidicPhLevel;
        freedomOfDefect += ingredients[i].FreedomForDefects;
        size += ingredients[i].Size;
        calories += ingredients[i].Calories;
      }

      //2. normalize the values
      var normalizedAcidity = acidity / ingredients.Count;
      var normalizedFOD = freedomOfDefect / ingredients.Count;
      var normalizedSize = size / ingredients.Count;
      var normalizedCalories = calories / ingredients.Count;
      
      //3. give weight to values
      computedToxicity = (0.15f * normalizedAcidity) + (0.35f * normalizedFOD) +  (0.35f * normalizedSize) + (0.15f * normalizedCalories);
      
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

      var normalizedVitamins=  vitamins / ingredients.Count;
      var normalizedMinerals = minerals / ingredients.Count;
      var normalizedSize = size / ingredients.Count;
      var normalizedAcidicValue = acidicValue / ingredients.Count > 0.7f || acidicValue / ingredients.Count < 0.3f
        ? -1f
        : acidicValue / ingredients.Count;
      var normalizedBitterness = bitterness / ingredients.Count;
      var normalizedSourSweetLevel = sourSweetLevel / ingredients.Count;
      //Salt if above 0.8f or below 0.2f = yuk! => -1f!
      var normalizedSaltiness = saltiness / ingredients.Count > 0.8f || saltiness / ingredients.Count < 0.2f
        ? -1f
        : saltiness / ingredients.Count;

      computedYumminess = (normalizedVitamins * 0.05f)  +
                          (normalizedMinerals * 0.05f) +
                          (normalizedSize * 0.1f) +
                          (normalizedAcidicValue * 0.2f) +
                          (normalizedBitterness * 0.2f * -1f) +
                          (normalizedSourSweetLevel * 0.2f) +
                          (normalizedSaltiness * 0.2f);

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