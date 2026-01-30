using System.Collections;
using System.Collections.Generic;
using SKCell;
using UnityEngine;

public class EndGalleryPanel : SKMonoSingleton<EndGalleryPanel>
{
    [Header("Buttons")] [SerializeField] private SKButton backButton;
    [SerializeField] private SKButton end1Button;
    [SerializeField] private SKButton end2Button;
    [SerializeField] private SKButton end3Button;

    [Header("Sprites")]
    [SerializeField] private Sprite end1LockImage;
    [SerializeField] private Sprite end1UnlockImage;
    [SerializeField] private Sprite end2LockImage;
    [SerializeField] private Sprite end2UnlockImage;
    [SerializeField] private Sprite end3LockImage;
    [SerializeField] private Sprite end3UnlockImage;

    [Header("Panel")] [SerializeField] private GameObject end1Gallery;
    [SerializeField] private GameObject end2Gallery;
    [SerializeField] private GameObject end3Gallery;

    [SerializeField] private GameObject end1Way;
    [SerializeField] private GameObject end2Way;
    [SerializeField] private GameObject end3Way;
    // Start is called before the first frame update
    void Start()
    {
        end1Button.gameObject.GetComponent<SKImage>().sprite = GameManager.end1 ? end1LockImage : end1UnlockImage;
        end2Button.gameObject.GetComponent<SKImage>().sprite = GameManager.end2 ? end2LockImage : end2UnlockImage;
        end3Button.gameObject.GetComponent<SKImage>().sprite = GameManager.end3 ? end3LockImage : end3UnlockImage;
        backButton.AddListener(SKButtonEventType.OnPressed, Back);
        end1Button.AddListener(SKButtonEventType.OnPressed, OpenEnd1);
        end2Button.AddListener(SKButtonEventType.OnPressed, OpenEnd2);
        end3Button.AddListener(SKButtonEventType.OnPressed, OpenEnd3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region ButtonEvents
    // TODO: 弹出动画
    void OpenEnd1()
    {
        if (GameManager.end1)
        {
            end1Gallery.SetActive(true);
        }
        else
        {
            end1Way.SetActive(true);
        }
    }

    void OpenEnd2()
    {
        if (GameManager.end2)
        {
            end2Gallery.SetActive(true);
        }
        else
        {
            end2Way.SetActive(true);
        }
    }

    void OpenEnd3()
    {
        if (GameManager.end3)
        {
            end3Gallery.SetActive(true);
        }
        else
        {
            end3Way.SetActive(true);
        }
    }

    void Back()
    {
        // SKAudioManager.instance.PlaySound();
        this.gameObject.SetActive(false);
    }
    #endregion
}
