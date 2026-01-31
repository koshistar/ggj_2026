using System;
using System.Collections;
using System.Collections.Generic;
using SKCell;
using UnityEngine;

public class AboutPanel : SKMonoSingleton<AboutPanel>
{
    [SerializeField] private SKButton backButton;

    [SerializeField] GameObject maskPanel;
    private void Start()
    {
        backButton.AddListener(SKButtonEventType.OnPressed, Back);
    }

    private void OnDisable()
    {
        maskPanel.SetActive(false);
        UIManager.Instance.SetPanel(null);
    }

    void Back()
    {
        // SKAudioManager.instance.PlaySound();
        this.gameObject.SetActive(false);
    }
}
