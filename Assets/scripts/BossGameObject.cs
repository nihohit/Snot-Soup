using UnityEngine;
using UnityEngine.UI;

namespace SnotSoup {

  public class BossGameObject : MonoBehaviour {
    private Slider healthSlider;
    private Slider hangerSlider;

    private void Start() {
      var slider = GameObject.Find("HealthSlider");
      healthSlider = slider.GetComponent<Slider>();
      slider = GameObject.Find("HangerSlider");
      hangerSlider = GameObject.Find("HangerSlider").GetComponent<Slider>();
    }

    private void Update() {
      Boss.Tick();
      healthSlider.value = Boss.Health / Boss.MAX_HEALTH;
      hangerSlider.value = Boss.Hangriness / Boss.MAX_HANGER;
    }
  }

}