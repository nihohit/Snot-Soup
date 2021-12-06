using UnityEngine;
using UnityEngine.UI;

namespace SnotSoup {

  public enum FeedingResponse { AteWell, AteFeelingBad, Refused };

  public class BossGameObject : MonoBehaviour {
    private Slider healthSlider;
    private Slider hangerSlider;

    private Mood lastMood = Mood.Happy;

    private void Start() {
      var slider = GameObject.Find("HealthSlider");
      healthSlider = slider.GetComponent<Slider>();
      slider = GameObject.Find("HangerSlider");
      hangerSlider = GameObject.Find("HangerSlider").GetComponent<Slider>();
      Boss.Reset();
    }

    private void Update() {
      if (lastMood == Mood.Dead) {
        return;
      }

      Boss.Tick();
      healthSlider.value = Boss.Health / Boss.MAX_HEALTH;
      hangerSlider.value = Boss.Hangriness / Boss.MAX_HANGER;

      var mood = Boss.getMood();
      if (mood == lastMood) {
        return;
      }
      // TODO - do something
      lastMood = mood;
    }

    public FeedingResponse TryFeed(FinishedSoup soup) {
      if (!Boss.willingToEat(soup)) {
        return FeedingResponse.Refused;
      }
      Boss.Feed(soup);
      return soup.filling > soup.toxicity ? FeedingResponse.AteWell : FeedingResponse.AteFeelingBad;
    }
  }
}