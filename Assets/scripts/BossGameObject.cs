using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
namespace SnotSoup {

  public enum FeedingResponse { AteWell, AteFeelingBad, Refused };

  public class BossGameObject : MonoBehaviour {
    private Slider healthSlider;
    private Slider hangerSlider;

    private Mood lastMood = Mood.Happy;
    private bool slideranimationInProgress = false;

        [SerializeField] Animator _anim;

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
      if (!slideranimationInProgress) {
        hangerSlider.value = Boss.Hangriness / Boss.MAX_HANGER;
      }

      var mood = Boss.getMood();
      if (mood == lastMood) {
        return;
      }
      if (mood == Mood.EatingThePlayer) {
        SceneManager.LoadScene("LoseScreen");
      }
      // TODO - do something
      lastMood = mood;
    }

    private IEnumerator UpdateSliders(float initialHangriness, float initialHealth) {
      slideranimationInProgress = true;
      var duration = 1.5f;
      var initialTime = System.DateTime.Now;
      var offset = 0f;
      while (offset <= duration) {
        yield return null;
        offset = (float)(System.DateTime.Now - initialTime).TotalSeconds;
        hangerSlider.value = Mathf.Lerp(initialHangriness, Boss.Hangriness, offset / duration) / Boss.MAX_HANGER;
        healthSlider.value = Mathf.Lerp(initialHealth, Boss.Health, offset / duration) / Boss.MAX_HEALTH;
      }
      if (Boss.Health <= 0) {
                _anim.SetBool("Dead", true);
      }
      slideranimationInProgress = false;
    }

    public FeedingResponse TryFeed(FinishedSoup soup) {
      var hangriness = Boss.Hangriness;
      var health = Boss.Health;
      Boss.Feed(soup);
            _anim.SetTrigger("Eat");
      StartCoroutine(UpdateSliders(hangriness, health));
      return soup.filling > soup.toxicity ? FeedingResponse.AteWell : FeedingResponse.AteFeelingBad;
    }

    public void Died()
    {
        SceneManager.LoadScene("WinScreen");
    }
  }
}