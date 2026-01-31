using System.Collections;
using System.Collections.Generic;
using SKCell;
using UnityEngine;

public class MainPenel : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private SKButton pauseButton;
    [SerializeField] private SKText countdownText;
    [SerializeField] private SKImage mask;
    [SerializeField] private SKSlider sanValue;
    
    [Header("Panel")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject overPanel;
    [Header("Countdown")] [SerializeField] private float countdown = 10f;
    // Start is called before the first frame update
    void Start()
    {
        pauseButton.AddListener(SKButtonEventType.OnPressed, Pause);
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown > 0f)
        {
            countdown -= Time.deltaTime;
            countdownText.SetText(countdown.ToString());
        }
        else
        {
            overPanel.SetActive(true);
        }
    }

    #region ButtonEvent

    void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        Player.instance.changeUIMap();
        UIManager.Instance.SetPanel(pausePanel);
    }

    #endregion
    
}
