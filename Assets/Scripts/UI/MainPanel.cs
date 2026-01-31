using System.Collections; 
using System.Collections.Generic; 
using SKCell; using UnityEngine; 
public class MainPanel : MonoBehaviour 
{ 
    [Header("UI Elements")] 
    [SerializeField] private SKButton pauseButton; 
    [SerializeField] private SKText countdownText; 
    [SerializeField] private SKImage mask; 
    [SerializeField] private SKSlider sanValue;
    
    [Header("Panel")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject overPanel;

    [Header("Mask")] 
    [SerializeField] private Sprite canUseMask;
    [SerializeField] private Sprite cannotUseMask;
    // Start is called before the first frame update
    void Start()
    {
        pauseButton.AddListener(SKButtonEventType.OnPressed, Pause);
    }

    // Update is called once per frame
    void Update()
    {
        if ( GameManager.Instance.escapeTime > 0f)
        {
            //GameManager.Instance.escapeTime -= Time.deltaTime;
            countdownText.SetText(GameManager.Instance.escapeTime.ToString());
        }
        else
        {
            overPanel.SetActive(true);
        }
        sanValue.SetValue(Player.instance.GetCurrentSanValue());
        if (sanValue.value <= 0)
        {
            overPanel.SetActive(true);
        }
        if(!Player.instance.CheckSanCanUseMask())
            mask.sprite = cannotUseMask;
        else
            mask.sprite = canUseMask;
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
