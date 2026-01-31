using System.Collections;
using SKCell;
using UnityEngine;

public class StartMenu : SKMonoSingleton<StartMenu>
{
    [Header("Buttons")]
    [SerializeField] private SKButton startButton;
    [SerializeField] private SKButton endGalleryButton;
    [SerializeField] private SKButton settingButton;
    [SerializeField] private SKButton aboutButton;
    [SerializeField] private SKButton exitButton;

    [Header("Panels")] [SerializeField] private GameObject endGalleryPanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject aboutPanel;
    [SerializeField] private GameObject maskPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        startButton.AddListener(SKButtonEventType.OnPressed, StartGame);
        endGalleryButton.AddListener(SKButtonEventType.OnPressed, EndGallery);
        settingButton.AddListener(SKButtonEventType.OnPressed, Setting);
        aboutButton.AddListener(SKButtonEventType.OnPressed, About);
        exitButton.AddListener(SKButtonEventType.OnPressed, Quit);
    }
    
    #region ButtonEvents
    void StartGame()
    {
        StartCoroutine(FlickerCoroutine());
        // SKAudioManager.instance.PlaySound();
        SceneLoader.Instance.Load("SampleScene");
    }
    
    void EndGallery()
    {
        // SKAudioManager.instance.PlaySound();
        endGalleryPanel.SetActive(true);
        UIManager.Instance.SetPanel(endGalleryPanel);
    }

    void Setting()
    {
        // SKAudioManager.instance.PlaySound();
        maskPanel.SetActive(true);
        settingPanel.SetActive(true);
        UIManager.Instance.SetPanel(settingPanel);
    }

    void About()
    {
        // SKAudioManager.instance.PlaySound();
        maskPanel.SetActive(true);
        aboutPanel.SetActive(true);
        UIManager.Instance.SetPanel(aboutPanel);
    }
    void Quit()
    {
        StartCoroutine(QuitCoroutine());
    }
    #endregion

    #region Coroutine

    IEnumerator FlickerCoroutine()
    {
        yield return null;
    }

    IEnumerator QuitCoroutine()
    {
        // SKAudioManager.instance.PlaySound();
        yield return new WaitForSeconds(1.0f);
        Application.Quit();
    }

    #endregion
}
