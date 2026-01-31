using System.Collections;
using System.Collections.Generic;
using SKCell;
using UnityEngine;

public class OverPanel : MonoBehaviour
{
    [Header("Buttons")] 
    [SerializeField] private SKButton backButton;
    [SerializeField] private SKButton retryButton;
    // Start is called before the first frame update
    void Start()
    {
        backButton.AddListener(SKButtonEventType.OnPressed, Back);
        retryButton.AddListener(SKButtonEventType.OnPressed, Retry);
    }

    #region ButtonEvent

    void Back()
    {
        gameObject.SetActive(false);
        SceneLoader.Instance.Load("MainMenu");
    }

    void Retry()
    {
        gameObject.SetActive(false);
        SceneLoader.Instance.Load("SampleScene");
    }

    #endregion
}
