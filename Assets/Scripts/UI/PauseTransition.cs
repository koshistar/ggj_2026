using SKCell;
using UnityEngine;

public class PauseTransition : MonoBehaviour
{
    public Sprite originSprite;
    public Sprite transitionSprite;

    public SKImage image;

    public void ChangeImage()
    {
        image.sprite = transitionSprite;
    }

    public void BackImage()
    {
        image.sprite = originSprite;
    }
}
