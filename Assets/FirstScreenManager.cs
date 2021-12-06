using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScreenManager : MonoBehaviour {
  void Start() {
    if (Logo != null) {
      Logo.SetActive(false);
    }
  }
  public GameObject Logo;

  private IEnumerator loadScene() {
    var initialTime = System.DateTime.Now;
    var scale = 0.75f;
    Logo.transform.localScale = Vector3.one * scale;
    while ((System.DateTime.Now - initialTime).TotalSeconds < 1) {
      var delta = Time.deltaTime / 2;
      Logo.transform.localScale += Vector3.one * delta; ;
      yield return null;
    }
    SceneManager.LoadScene("CookingScene");
  }

  public void LoadNewScene() {
    Logo.SetActive(true);
    StartCoroutine(loadScene());
  }

  public void LoadMainMenu() {
    SceneManager.LoadScene("IntroScene");
  }
}
