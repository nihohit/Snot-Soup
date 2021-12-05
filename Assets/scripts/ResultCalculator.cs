namespace Soupish {

  public class FinishedSoup {
    public float toxicity;
    public float yumminess;
    public float looks;
  }

  public class CookingInputs {

  }

  public class ResultCalculator {
    private static float computeToxicity(CookingInputs inputs) {
      return 1.0f;
    }

    private static float computeYumminess(CookingInputs inputs) {
      return 1.0f;
    }

    private static float computeLooks(CookingInputs inputs) {
      return 1.0f;
    }

    public static FinishedSoup getSoupResult(CookingInputs inputs) {
      return new FinishedSoup{
        toxicity = computeToxicity(inputs),
        yumminess = computeYumminess(inputs),
        looks = computeLooks(inputs)
      };
    }
  }

}