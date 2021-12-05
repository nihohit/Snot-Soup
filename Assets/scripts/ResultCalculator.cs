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
      return 0.1f;
    }

    private static float computeYumminess(CookingInputs inputs) {
      return 0.1f;
    }

    private static float computeLooks(CookingInputs inputs) {
      return 1.0f;
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