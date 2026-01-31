using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class CustomSlider : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerDownHandler, IPointerUpHandler,
    IPointerExitHandler
{
    [Header("缩放配置")]
    [Tooltip("选中后的目标缩放比例")]
    public Vector3 targetScale = new Vector3(1.1f, 1.1f, 1.1f);
    [Tooltip("缩放平滑速度，值越大过渡越快")]
    public float scaleSmoothSpeed = 10f;
    [Header("填充色加深配置")]
    [Tooltip("按住时填充色的加深系数（0-1，值越大颜色越深，建议0.2-0.4）")]
    [Range(0f, 1f)] public float darkenIntensity = 0.3f;
    [Tooltip("颜色过渡平滑速度，值越大变化越快")]
    public float colorSmoothSpeed = 15f;

    private Vector3 _originalScale;
    private Vector3 _currentTargetScale;
    private Slider _slider;
    private Image _fillImage;
    private Color _originalFillColor;
    private Color _targetFillColor;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _originalScale = transform.localScale;
        _currentTargetScale = _originalScale;
        Transform fillTransform = transform.Find("Fill Area/Fill");
        if (fillTransform != null)
        {
            _fillImage= fillTransform.GetComponent<Image>();
            _originalFillColor = _fillImage.color;
            _targetFillColor = _fillImage.color;
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.localScale =
            Mathf.Lerp(transform.localScale.x, _currentTargetScale.x, scaleSmoothSpeed * Time.deltaTime) * Vector3.one;
        if (_fillImage != null && _fillImage.color != _targetFillColor)
        {
            _fillImage.color = Color.Lerp(_fillImage.color, _targetFillColor, colorSmoothSpeed * Time.deltaTime);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        _currentTargetScale = targetScale;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _currentTargetScale = _originalScale;
    }

    public void SetValue(float value)
    {
        _slider.value = value;
    }

    public float GetValue() => _slider.value;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_fillImage != null)
        {
            Color darkenedColor = new Color(_originalFillColor.r * (1 - darkenIntensity),
                _originalFillColor.g * (1 - darkenIntensity), _originalFillColor.b * (1 - darkenIntensity),
                _originalFillColor.a);
            _targetFillColor = darkenedColor;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetFillColor();
    }

    void ResetFillColor()
    {
        if (_fillImage != null)
        {
            _targetFillColor = _originalFillColor;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        // ResetFillColor();
    }
}
