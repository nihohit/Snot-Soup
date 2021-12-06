using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ESCMenu : MonoBehaviour
{
    [SerializeField] GameObject _canvas;

    [SerializeField] Button _mainMenuButton;
    [SerializeField] Button _resumeButton;
    [SerializeField] Slider _volumeSlider;

    private static float _volume = 1;

    public static ESCMenu Instance;

    public bool IsOn { get { return _canvas.activeSelf; } private set { } }

    protected void Awake()
    {
        Instance = this;
        _mainMenuButton.onClick.AddListener(LoadMainMenu);
        _resumeButton.onClick.AddListener(Resume);
        _volumeSlider.onValueChanged.AddListener(SetVolume);
        _volumeSlider.value = _volume;
        SetVolume(_volume);
    }

    public void ToggleActive(bool val)
    {
        _canvas.SetActive(val);
    }

    private void SetVolume(float value)
    {
        _volume = value;
        AudioListener.volume = value;
    }

    private void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("IntroScene");
    }

    public event Action OnResume;
    private void Resume()
    {
        OnResume?.Invoke();
    }
}
