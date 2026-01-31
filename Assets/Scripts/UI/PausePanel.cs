using System;
using System.Collections;
using System.Collections.Generic;
using SKCell;
using UnityEngine;

public class PausePanel : SKMonoSingleton<PausePanel>
{
    [Header("Buttons")] [SerializeField] private SKButton ContinueButton;
    [SerializeField] private SKButton RetryButton;
    [SerializeField] private SKButton ExitButton;

    // Start is called before the first frame update
    void Start()
    {
        ContinueButton.AddListener(SKButtonEventType.OnPressed, Continue);
        RetryButton.AddListener(SKButtonEventType.OnPressed, Retry);
        ExitButton.AddListener(SKButtonEventType.OnPressed, Exit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        Player.instance.ChangeGamePlayMap();
        UIManager.Instance.SetPanel(null);
    }

    #region buttonEvent

    private void Continue()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    void Retry()
    {
        gameObject.SetActive(false);
        Player.instance.DisableAllInput();
        SceneLoader.Instance.Load("SampleScene");
    }

    void Exit()
    {
        gameObject.SetActive(false);
        SceneLoader.Instance.Load("MainMenu");
    }
    #endregion
}
