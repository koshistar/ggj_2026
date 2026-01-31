using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class SpriteYSort : MonoBehaviour
{
    public int minOrder = 1;
    public int maxOrder = 99;

    // 阈值比例，0.3~0.5 最自然
    [Range(0f, 0.5f)]
    public float hysteresis = 0.4f;

    SpriteRenderer sr;
    Collider2D col;

    int currentOrder = -1;
    float currentBandMin;
    float currentBandMax;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    void LateUpdate()
    {
        float footY = col != null
            ? col.bounds.min.y
            : sr.bounds.min.y;

        float viewY = Camera.main.WorldToViewportPoint(
            new Vector3(0, footY, 0)
        ).y;

        viewY = Mathf.Clamp01(viewY);

        int levels = maxOrder - minOrder + 1;
        float bandSize = 1f / levels;

        // 当前 Y 不在安全区，才重新计算
        if (currentOrder < 0 ||
            viewY < currentBandMin ||
            viewY > currentBandMax)
        {
            int newOrder = maxOrder - Mathf.FloorToInt(viewY / bandSize);
            newOrder = Mathf.Clamp(newOrder, minOrder, maxOrder);

            currentOrder = newOrder;
            sr.sortingOrder = currentOrder;

            // 扩展安全区（死区）
            float center = (maxOrder - currentOrder + 0.5f) * bandSize;
            float halfSafe = bandSize * hysteresis;

            currentBandMin = center - halfSafe;
            currentBandMax = center + halfSafe;
        }
    }
}

