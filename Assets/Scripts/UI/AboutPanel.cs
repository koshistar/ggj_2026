using System;
using System.Collections;
using System.Collections.Generic;
using SKCell;
using UnityEngine;

public class AboutPanel : SKMonoSingleton<AboutPanel>
{
    [SerializeField] private SKButton backButton;

    private void Start()
    {
        backButton.AddListener(SKButtonEventType.OnPressed, Back);
    }

    void Back()
    {
        // SKAudioManager.instance.PlaySound();
        this.gameObject.SetActive(false);
        UIManager.Instance.SetPanel(null);
    }
}
