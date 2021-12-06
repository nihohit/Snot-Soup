using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScreenManager : MonoBehaviour {
  public void LoadNewScene() {
    SceneManager.LoadScene("CookingScene");
  }
}
