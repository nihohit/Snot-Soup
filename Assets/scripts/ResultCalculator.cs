using System.Collections.Generic;
using SnotSoup.Gameplay.Ingredients;
using UnityEngine;

namespace SnotSoup {

  public class FinishedSoup {
    public float toxicity;
    public float yumminess;
    public float filling;
  }

  public class CookingInputs {
    public List<IngredientModel> Ingredients = new List<IngredientModel>();

    public CookingInputs(List<IngredientModel> ingredients) {
      Ingredients = ingredients;
    }
  }

  public class ResultCalculator {
    private static float computeToxicity(CookingInputs inputs) {
      var ingredients = inputs.Ingredients;
      var acidity = 0f;
      var freedomOfDefect = 0f;
      var size = 0f;

      //1. first calculate the values into one value "pool"
      foreach (var ingredient in inputs.Ingredients) {
        acidity += ingredient.AcidicPhLevel * ingredient.Size;
        freedomOfDefect += ingredient.FreedomForDefects * ingredient.Size;
        size += ingredient.Size;
      }

      //2. normalize the values
      var normalizedAcidity = Mathf.Abs(acidity) / size;
      var normalizedFOD = freedomOfDefect / size;

      //3. give weight to values
      var computedToxicity = (0.5f * normalizedAcidity) + (0.5f * normalizedFOD);
      return computedToxicity;
    }

    private static float computeYumminess(CookingInputs inputs) {
      var ingredients = inputs.Ingredients;
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

      var normalizedVitamins = vitamins / ingredients.Count;
      var normalizedMinerals = minerals / ingredients.Count;
      var normalizedSize = size / ingredients.Count;
      var normalizedAcidicValue = acidicValue / ingredients.Count > 0.7f || acidicValue / ingredients.Count < 0.3f
        ? -1f
        : acidicValue / ingredients.Count;
      var normalizedBitterness = bitterness / ingredients.Count;
      var normalizedSourSweetLevel = sourSweetLevel / ingredients.Count > 0.7f || sourSweetLevel / ingredients.Count < -0.7f
        ? -1f
        : sourSweetLevel / ingredients.Count;
      //Salt if above 0.8f or below 0.2f = yuk! => -1f!
      var normalizedSaltiness = saltiness / ingredients.Count > 0.8f || saltiness / ingredients.Count < 0.2f
        ? -1f
        : saltiness / ingredients.Count;
      var variety = 0.015f * ingredients.Count;

      var computedYumminess = (normalizedVitamins * 0.05f) +
                          (normalizedMinerals * 0.05f) +
                          (normalizedSize * 0.1f) +
                          (normalizedAcidicValue * 0.2f) +
                          (normalizedBitterness * 0.2f) +
                          (normalizedSourSweetLevel * 0.2f) +
                          (normalizedSaltiness * 0.2f) + variety;

      return computedYumminess;
    }

    private static float computeFilling(CookingInputs inputs) {
      var ingredients = inputs.Ingredients;
      var calories = 0f;
      var viscosity = 0f;
      var size = 0f;

      foreach (var ingredient in inputs.Ingredients) {
        calories += ingredient.Calories * ingredient.Size;
        viscosity += ingredient.Viscosity * ingredient.Size;
        size += ingredient.Size;
      }

      var normalizedCalories = calories / size;
      var normalizedViscosity = viscosity / size;

      var computedLooks = (normalizedCalories * 0.7f) + (normalizedViscosity * 0.3f);

      return computedLooks;
    }

    public static FinishedSoup getSoupResult(CookingInputs inputs) {
      return new FinishedSoup {
        toxicity = computeToxicity(inputs),
        yumminess = computeYumminess(inputs),
        filling = computeFilling(inputs)
      };
    }
  }
}