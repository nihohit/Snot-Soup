using UnityEngine;

namespace SnotSoup {

  public enum Mood { Happy, Hangry, Sick, Dead, EatingThePlayer }

  public class Boss {
    public const float MAX_HEALTH = 100;
    public const float MAX_HANGER = 150;
    public const float CRANKY_HANGER = 120;

    public static float Health { get; private set; } = 100f; // 0..100
    public static float Hangriness { get; private set; } = 0f; // 0..150

    public static void Reset() {
      Health = MAX_HEALTH;
      Hangriness = 0f;
    }

    public static void Tick() {
      Hangriness += Time.deltaTime * 2.5f;
    }

    public static void Feed(FinishedSoup soup) {
      Health -= soup.toxicity * MAX_HEALTH;
      Health = Mathf.Max(Health, 0);
      Hangriness -= soup.filling * MAX_HANGER;
      Hangriness = Mathf.Max(Hangriness, 0);
    }

    public static Mood getMood() {
      if (Health <= 0) {
        return Mood.Dead;
      }
      if (Hangriness >= MAX_HANGER) {
        return Mood.EatingThePlayer;
      }

      if (Health <= 20) {
        return Mood.Sick;
      }

      if (Hangriness >= CRANKY_HANGER) {
        return Mood.Hangry;
      }

      return Mood.Happy;
    }

    public static bool willingToEat(FinishedSoup soup) {
      return soup.yumminess > 0.2;
    }
  }

}