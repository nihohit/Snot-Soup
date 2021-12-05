using UnityEngine;

namespace SnotSoup {

  public enum Mood { Happy, Hangry, Sick, Dead, EatingThePlayer }

  public class Boss {
    public static float Health { get; private set; } // 0..100
    public static float Hangriness { get; private set; } // 0..150

    public static void Tick() {
      Hangriness -= Time.deltaTime;
    }

    public static void Feed(FinishedSoup soup) {
      Health -= soup.toxicity;
      Hangriness += soup.yumminess;
    }

    public static Mood getMood() {
      if (Health <= 0) {
        return Mood.Dead;
      }
      if (Hangriness <= 0) {
        return Mood.EatingThePlayer;
      }

      if (Health <= 20) {
        return Mood.Sick;
      }

      if (Hangriness <= 20) {
        return Mood.Hangry;
      }

      return Mood.Happy;
    }

    public static bool willingToEat(FinishedSoup soup) {
      return soup.looks > 20;
    }
  }

}