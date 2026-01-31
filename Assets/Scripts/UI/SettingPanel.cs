using System.Collections;
using System.Collections.Generic;
using SKCell;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : SKMonoSingleton<SettingPanel>
{
    [Header("UI Elements")]
    [SerializeField] private CustomSlider totalVolume;
    [SerializeField] private CustomSlider musicVolume;
    [SerializeField] private CustomSlider sfxVolume;
    [SerializeField] private SKButton backButton;
    
    // Start is called before the first frame update
    void Start()
    {
        totalVolume.SetValue(AudioSystem.Instance.GetListenerVolume());
        musicVolume.SetValue(AudioSystem.Instance.GetMusicVolume());
        sfxVolume.SetValue(AudioSystem.Instance.GetSfxVolume());
        backButton.AddListener(SKButtonEventType.OnPressed, Back);
    }

    // Update is called once per frame
    void Update()
    {
        AudioSystem.Instance.SetListenerVolume(totalVolume.GetValue());
        AudioSystem.Instance.SetMusicVolume(musicVolume.GetValue());
        AudioSystem.Instance.SetSfxVolume(sfxVolume.GetValue());
    }

    #region ButtonEvents

    void Back()
    {
        gameObject.SetActive(false);
        UIManager.Instance.SetPanel(null);
    }

    #endregion
}
